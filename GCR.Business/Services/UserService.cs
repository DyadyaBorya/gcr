using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using GCR.Business.Security;
using GCR.Core.Entities;
using GCR.Core.Repositories;
using GCR.Core.Security;
using GCR.Core.Services;
using GCR.Core;
using System.Transactions;


namespace GCR.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private ISecurityProvider userSecurity;

        public UserService(IUserRepository repo, ISecurityProvider security)
        {
            userRepository = repo;
            userSecurity = security;
        }

        public bool LoginLocal(string username, string password, bool persist)
        {
            return userSecurity.LoginLocal(username, password, persist);
        }

        public void Logout()
        {
            userSecurity.Logout();
        }

        public void CreateLocalAccount(string username, string password)
        {
            userSecurity.CreateLocalAccount(username, password);
        }

        public bool CreateOAuthAccount(string username, string encryptedLoginData)
        {
            return userSecurity.CreateOAuthAccount(username, encryptedLoginData);
        }

        public bool CreateOAuthAccount(string username, string provider, string providerUserId)
        {

            // Check if user already exists
            if (this.UsernameExists(username))
            {
                // Insert name into the profile table
                userRepository.Create(new UserProfile { UserName = username });
                userRepository.SaveChanges();

                userSecurity.CreateOAuthAccount(provider, providerUserId, username);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateOAuthAccount(string username, string encryptedLoginData)
        {
            return userSecurity.UpdateOAuthAccount(username, encryptedLoginData);
        }

        public bool UpdateOAuthAccount(string username, string provider, string providerUserId)
        {
            userSecurity.UpdateOAuthAccount(provider, providerUserId, username);
            return true;
        }

        public OAuthResult GetOAuthResultFromRequest(string returnUrl)
        {
            return userSecurity.GetOAuthResultFromRequest(returnUrl);
        }

        public OAuthResult GetOAuthResult(string encryptedLoginData)
        {
            return userSecurity.GetOAuthResult(encryptedLoginData);
        }

        public bool LoginOAuth(string encryptedLoginData)
        {
            return userSecurity.LoginOAuth(encryptedLoginData);
        }

        public bool LoginOAuth(string providerName, string providerUserId)
        {
            return userSecurity.LoginOAuth(providerName, providerUserId);
        }

        public bool UsernameExists(string username)
        {
            return userRepository.Query.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower()) != null;
        }

        public bool Disassociate(string provider, string providerUserId)
        {
            return userSecurity.Disassociate(provider, providerUserId);
        }

        public IEnumerable<OAuthProvider> GetOAuthProviders()
        {
            return userSecurity.GetOAuthProviders();
        }

        public IEnumerable<OAuthProvider> GetOAuthAccountsForUser(string username)
        {
            return userSecurity.GetOAuthAccountsForUser(username);
        }

        public void RequestAuthentication(string provider, string returnUrl)
        {
            userSecurity.RequestAuthentication(provider, returnUrl);
        }

        public bool HasLocalAccount(int userId)
        {
            return userSecurity.HasLocalAccount(userId);
        }

        public bool ChangeLocalPassword(string userName, string oldPassword, string newPassword)
        {
            return userSecurity.ChangeLocalPassword(userName, oldPassword, newPassword);
        }
    }
}
