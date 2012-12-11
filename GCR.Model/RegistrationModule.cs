using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using GCR.Core.Repositories;
using GCR.Model.Repositories;

namespace GCR.Model
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IMemberRepository>().To<MemberRepository>();
            Bind<INewsRepository>().To<NewsRepository>();
            Bind<IScheduleRepository>().To<ScheduleRepository>();
            Bind<ISeasonRepository>().To<SeasonRepository>();
            Bind<ITeamRepository>().To<TeamRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
