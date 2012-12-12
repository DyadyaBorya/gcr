using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Security
{
    /// <summary>
    /// Represents a custom principal.
    /// </summary>
    /// <remarks></remarks>
    public class CustomPrincipal : GenericPrincipal
    {

        private string[] _roles;
        /// <summary>
        /// Initializes a new instance of the CustomPrincipal class from a user identity and an 
        /// array of role names to which the user represented by that identity belongs.
        /// </summary>
        /// <param name="identity">Identity that represents any user. </param>
        /// <param name="roles">An array of roles to which the user belongs.</param>

        internal CustomPrincipal(CustomIdentity identity, string[] roles)
            : base(identity, roles)
        {
            _roles = roles;
        }

    /// <summary>
    /// Gets collection of roles the user belongs.
    /// </summary>
    public IEnumerable<string> GetRoles()
	{

		return from r in _roles select r;
	}

    }

    /// <summary>
    /// Represents a custom user.
    /// </summary>
    public class CustomIdentity : GenericIdentity
    {

        private int _userId;

        private string _displayName;
        /// <summary>
        /// Unique Id of User.
        /// </summary>
        public int UserId
        {
            get { return _userId; }
        }

        /// <summary>
        /// Display name of User.
        /// </summary>
        public string DisplayName
        {
            get { return _displayName; }
        }

        /// <summary>
        /// Initializes a new instance of the CustomIdentity class representing the user.
        /// </summary>
        /// <param name="name">Unique Username of User.</param>
        /// <param name="userId">Unique Id of User.</param>
        /// <param name="displayName">Display name of User.</param>
        internal CustomIdentity(string name, int userId, string displayName):base(name)
        {
            _userId = userId;
            _displayName = displayName;
        }

    }
}
