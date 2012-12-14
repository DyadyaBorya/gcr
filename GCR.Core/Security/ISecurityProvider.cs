using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Security
{
    public interface ISecurityProvider
    {
        bool LoginLocal(string username, string password, bool persist);

        void Logout();

        void CreateLocalAccount(string username, string password);

        bool Disassociate(string provider, string providerUserId);

        bool HasLocalAccount(int userId);

        bool ChangeLocalPassword(string userName, string oldPassword, string newPassword);

        bool LoginOAuth(string encryptedLoginData);

        bool LoginOAuth(string providerName, string providerUserId);

        bool CreateOAuthAccount(string username, string encryptedLoginData);

        void CreateOAuthAccount(string username, string provider, string providerUserId);

        bool UpdateOAuthAccount(string username, string encryptedLoginData);

        bool UpdateOAuthAccount(string username, string provider, string providerUserId);

        OAuthResult GetOAuthResultFromRequest(string returnUrl);

        OAuthResult GetOAuthResult(string encryptedLoginData);

        IEnumerable<OAuthProvider> GetOAuthProviders();

        IEnumerable<OAuthProvider> GetOAuthAccountsForUser(string username);

        void RequestAuthentication(string provider, string returnUrl);

        bool UserExists(string username);
    }
}
