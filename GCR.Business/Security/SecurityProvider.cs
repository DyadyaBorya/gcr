using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Security;
using GCR.Core.Services;
using WebMatrix.WebData;
using Microsoft.Web.WebPages.OAuth;
using GCR.Core;
using System.Transactions;
using DotNetOpenAuth.AspNet;
using System.Web.Security;
using System.IO;

namespace GCR.Business.Security
{
    internal class SecurityProvider : ISecurityProvider
    {
        public SecurityProvider()
        {
            InitializeSimpleMembership.Initialize();
        }

        public void RegisterOAuthProviders()
        {
            OAuthWebSecurity.RegisterGoogleClient();
        }

        public bool LoginLocal(string username, string password, bool persist)
        {
            return WebSecurity.Login(username, password, persistCookie: persist);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public void CreateLocalAccount(string username, string password)
        {
            try
            {
                if (WebSecurity.UserExists(username))
                {
                    WebSecurity.CreateAccount(username, password);
                }
                else
                {
                    WebSecurity.CreateUserAndAccount(username, password);
                }
            }
            catch (MembershipCreateUserException ex)
            {
                throw new UserCreationException(ErrorCodeToString(ex.StatusCode));
            }
        }

        public bool CreateOAuthAccount(string username, string encryptedLoginData)
        {
            string provider = null;
            string providerUserId = null;

            if (!TryDecryptProviderData(encryptedLoginData, out provider, out providerUserId))
            {
                return false;
            }

            CreateOAuthAccount(username, provider, providerUserId);
            return true;
        }

        public void CreateOAuthAccount(string username, string provider, string providerUserId)
        {
            OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, username);
        }

        public bool UpdateOAuthAccount(string username, string encryptedLoginData)
        {
            string provider = null;
            string providerUserId = null;

            if (!TryDecryptProviderData(encryptedLoginData, out provider, out providerUserId))
            {
                return false;
            }

            return UpdateOAuthAccount(username, provider, providerUserId);
        }

        public bool UpdateOAuthAccount(string username, string provider, string providerUserId)
        {
            OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, username);
            return true;
        }

        public OAuthResult GetOAuthResultFromRequest(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(returnUrl);
            var oAuth = new OAuthResult();

            if (!result.IsSuccessful)
            {
                return oAuth;
            }

            oAuth.IsValid = true;
            oAuth.UserName = result.UserName;
            oAuth.Provider = result.Provider;
            oAuth.ProviderUserId = result.ProviderUserId;
            oAuth.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            oAuth.EncryptedLoginData = EncryptProviderData(result.Provider, result.ProviderUserId);

            return oAuth;
        }

        public OAuthResult GetOAuthResult(string encryptedLoginData)
        {
            string provider = null;
            string providerUserId = null;
            var oAuth = new OAuthResult();

            if (!TryDecryptProviderData(encryptedLoginData, out provider, out providerUserId))
            {
                return oAuth;
            }

            
            oAuth.IsValid = true;
            oAuth.Provider = provider;
            oAuth.ProviderUserId = providerUserId;
            oAuth.UserName = OAuthWebSecurity.GetUserName(provider, providerUserId);
            oAuth.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            oAuth.EncryptedLoginData = encryptedLoginData;

            return oAuth;
        }


        public bool LoginOAuth(string encryptedLoginData)
        {
            string provider = null;
            string providerUserId = null;

            if (!TryDecryptProviderData(encryptedLoginData, out provider, out providerUserId))
            {
                return false;
            }
            return LoginOAuth(provider, providerUserId);
        }

        public bool LoginOAuth(string providerName, string providerUserId)
        {
            return OAuthWebSecurity.Login(providerName, providerUserId, createPersistentCookie: false);
        }

        public bool Disassociate(string provider, string providerUserId)
        {
            bool success = false;
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == CurrentUser.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new System.Transactions.TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(CurrentUser.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(CurrentUser.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<OAuthProvider> GetOAuthProviders()
        {
            var accounts = OAuthWebSecurity.RegisteredClientData;
            var providers = new List<OAuthProvider>();
            foreach (var acct in accounts)
            {
                var pro = new OAuthProvider();
                pro.ProviderName = acct.AuthenticationClient.ProviderName;
                pro.ProviderDisplayName = acct.DisplayName;
                providers.Add(pro);
            }

            return providers;
        }

        public IEnumerable<OAuthProvider> GetOAuthAccountsForUser(string username)
        {
            var accounts = OAuthWebSecurity.GetAccountsFromUserName(username);
            var providers = new List<OAuthProvider>();
            foreach (var acct in accounts)
            {
                var pro = new OAuthProvider();
                pro.ProviderName = acct.Provider;
                pro.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(acct.Provider).DisplayName;
                pro.ProviderUserId = acct.ProviderUserId;
                providers.Add(pro);
            }

            return providers;
        }

        public void RequestAuthentication(string provider, string returnUrl)
        {
            OAuthWebSecurity.RequestAuthentication(provider, returnUrl);
        }

        public bool HasLocalAccount(int userId)
        {
            return OAuthWebSecurity.HasLocalAccount(userId);
        }

        public bool ChangeLocalPassword(string userName, string oldPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(userName, oldPassword, newPassword);
        }

        public bool UserExists(string username)
        {
            return WebSecurity.UserExists(username);
        }

        public string EncryptData(string value)
        {
            return CryptoHelper.ProtectData(value);
        }

        public string DecryptData(string encrytedData)
        {
            string value;
            CryptoHelper.UnprotectData(encrytedData, out value);
            return value;
        }

        private string EncryptProviderData(string providerName, string providerUserId)
        {
            return OAuthWebSecurity.SerializeProviderUserId(providerName, providerUserId);
        }

        private bool TryDecryptProviderData(string data, out string providerName, out string providerUserId)
        {
            return OAuthWebSecurity.TryDeserializeProviderUserId(data, out providerName, out providerUserId);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        private static class CryptoHelper
        {
            public static string ProtectData(string value)
            {
                string str;
                using (MemoryStream stream = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(value);
                    writer.Flush();

                    byte[] dst = new byte[stream.Length];
                    Buffer.BlockCopy(stream.GetBuffer(), 0, dst, 0, (int)stream.Length);
                    byte[] result = MachineKey.Protect(dst, typeof(CryptoHelper).FullName, "User: " + CurrentUser.Identity.Name);
                    str = Convert.ToBase64String(result);
                }
                return str;
            }

            public static bool UnprotectData(string protectedData, out string value)
            {
                value = null;
                if (!string.IsNullOrEmpty(protectedData))
                {
                    byte[] src = Convert.FromBase64String(protectedData);
                    byte[] buffer = MachineKey.Unprotect(src, typeof(CryptoHelper).FullName, "User: " + CurrentUser.Identity.Name);
                    using (MemoryStream stream = new MemoryStream(buffer))
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        try
                        {

                            string str = reader.ReadString();
                            if (stream.ReadByte() == -1)
                            {
                                value = str;
                                return true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                return false;
            }
        }
    }
}
