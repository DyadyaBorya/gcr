using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GCR.Core.Services;
using GCR.Business.Services;
using GCR.Core.Security;
using GCR.Business.Security;

namespace GCR.Business
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IMemberService>().To<MemberService>();
            Bind<INewsService>().To<NewsService>();
            Bind<IScheduleService>().To<ScheduleService>();
            Bind<ISeasonService>().To<SeasonService>();
            Bind<ITeamService>().To<TeamService>();
            Bind<IUserService>().To<UserService>();
            Bind<ISecurityProvider>().To<SecurityProvider>();
        }
    }
}
