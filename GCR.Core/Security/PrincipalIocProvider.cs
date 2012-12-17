using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Security;
using Ninject;
using Ninject.Activation;
using GCR.Core.Repositories;
using GCR.Core.Entities;
using GCR.Core.Services;

namespace GCR.Core.Security
{
    internal class PrincipalIocProvider : Provider<CustomPrincipal>
    {
        protected override CustomPrincipal CreateInstance(IContext context)
        {
            UserProfile user = null;
            string username = CurrentUser.GetCurrentUserName();
            if (!string.IsNullOrEmpty(username))
            {
                var repo = context.Kernel.Get<IUserService>();
                user = repo.GetProfileByUsername(username);
            }

            int userId = user != null ? user.UserId: 0;
            string email = user != null ? user.UserName : null; //TODO;
            var identity = new CustomIdentity(username, userId, email);

            //Get array of roles
            var roles = Roles.GetRolesForUser(username);

            //'Create principal
            var principal = new CustomPrincipal(identity, roles);

            return principal;
        }
    }
}
