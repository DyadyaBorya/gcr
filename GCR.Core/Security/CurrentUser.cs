using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GCR.Core.Security;
using Ninject;

namespace GCR.Core
{

    /// <summary>
    /// Holds the current user for the application. Also tries to set the AppDomain Principal 
    /// so all Threads will have same Principal as their native Principal.
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// Object used for thread locking.
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// Indicates whether of not the a user has been authenticated yet.
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                return Principal != null && Principal.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// Get the  <see cref="CustomPrincipal"/> that contains a <see cref="CustomIdentity"/> that contains info about the user and also roles functionality.
        /// </summary>
        public static CustomPrincipal Principal
        {
            get
            {
                return IoC.Get<CustomPrincipal>();
            }
        }

        /// <summary>
        /// Get the <see cref="CustomIdentity"/> that contains info about the user.
        /// </summary>
        public static CustomIdentity Identity
        {
            get { return Principal.Identity as CustomIdentity; }
        }

        internal static string GetCurrentUserName()
        {
            if (System.Web.Hosting.HostingEnvironment.IsHosted)
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    return current.User.Identity.Name;
                }
            }

            IPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal;
            if (currentPrincipal != null && currentPrincipal.Identity != null)
            {
                return currentPrincipal.Identity.Name;
            }

            return string.Empty;
        }
    }
}
