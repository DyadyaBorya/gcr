using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Business.Security
{
    public class SecurityProvider
    {

        //public static bool ConfirmAccount(string accountConfirmationToken)
        //{
        //    return VerifyProvider().ConfirmAccount(accountConfirmationToken);
        //}

        //public static bool ConfirmAccount(string userName, string accountConfirmationToken)
        //{
        //    return VerifyProvider().ConfirmAccount(userName, accountConfirmationToken);
        //}

        //public static string CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        //{
        //    return VerifyProvider().CreateAccount(userName, password, requireConfirmationToken);
        //}

        //public static string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        //{
        //    ExtendedMembershipProvider provider = VerifyProvider();
        //    IDictionary<string, object> values = null;
        //    if (propertyValues != null)
        //    {
        //        values = new RouteValueDictionary(propertyValues);
        //    }
        //    return provider.CreateUserAndAccount(userName, password, requireConfirmationToken, values);
        //}

        //public static int GetUserId(string userName)
        //{
        //    VerifyProvider();
        //    MembershipUser user = Membership.GetUser(userName);
        //    if (user == null)
        //    {
        //        return -1;
        //    }
        //    return (int)user.ProviderUserKey;
        //}

        //public static int GetUserIdFromPasswordResetToken(string token)
        //{
        //    return VerifyProvider().GetUserIdFromPasswordResetToken(token);
        //}

        //public static void InitializeDatabaseConnection(string connectionStringName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        //{
        //    DatabaseConnectionInfo connect = new DatabaseConnectionInfo
        //    {
        //        ConnectionStringName = connectionStringName
        //    };
        //    InitializeProviders(connect, userTableName, userIdColumn, userNameColumn, autoCreateTables);
        //}

        //public static void InitializeDatabaseConnection(string connectionString, string providerName, string userTableName, string userIdColumn, string userNameColumn, bool autoCreateTables)
        //{
        //    DatabaseConnectionInfo connect = new DatabaseConnectionInfo
        //    {
        //        ConnectionString = connectionString,
        //        ProviderName = providerName
        //    };
        //    InitializeProviders(connect, userTableName, userIdColumn, userNameColumn, autoCreateTables);
        //}

        //public static bool IsAccountLockedOut(string userName, int allowedPasswordAttempts, int intervalInSeconds)
        //{
        //    VerifyProvider();
        //    return IsAccountLockedOut(userName, allowedPasswordAttempts, TimeSpan.FromSeconds((double)intervalInSeconds));
        //}

        //public static bool IsAccountLockedOut(string userName, int allowedPasswordAttempts, TimeSpan interval)
        //{
        //    return IsAccountLockedOutInternal(VerifyProvider(), userName, allowedPasswordAttempts, interval);
        //}

        //public static bool IsConfirmed(string userName)
        //{
        //    return VerifyProvider().IsConfirmed(userName);
        //}

        //public static bool IsCurrentUser(string userName)
        //{
        //    VerifyProvider();
        //    return string.Equals(CurrentUserName, userName, StringComparison.OrdinalIgnoreCase);
        //}

        //private static bool IsUserLoggedOn(int userId)
        //{
        //    VerifyProvider();
        //    return (CurrentUserId == userId);
        //}

        //public static bool Login(string userName, string password, bool persistCookie = false)
        //{
        //    VerifyProvider();
        //    bool flag = Membership.ValidateUser(userName, password);
        //    if (flag)
        //    {
        //        FormsAuthentication.SetAuthCookie(userName, persistCookie);
        //    }
        //    return flag;
        //}

        //public static void Logout()
        //{
        //    VerifyProvider();
        //    FormsAuthentication.SignOut();
        //}

        //public static bool UserExists(string userName)
        //{
        //    VerifyProvider();
        //    return (Membership.GetUser(userName) != null);
        //}

    }
}
