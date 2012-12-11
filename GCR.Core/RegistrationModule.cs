﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Identity;
using Ninject.Web.Common;
using Ninject.Modules;
using System.Web;

namespace GCR.Core
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
            if (System.Web.Hosting.HostingEnvironment.IsHosted)
            {
                Bind<CustomPrincipal>().ToProvider(new PrincipalProvider()).InRequestScope();
            }
            else
            {
                Bind<CustomPrincipal>().ToProvider(new PrincipalProvider()).InSingletonScope();
            }
        }
    }
}
