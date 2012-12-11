using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Web.Common;

namespace GCR.Core
{
    
    public static class IoC
    {
        private static IBootstrapper bootStrapper;
        public static void SetBootstrapper(IBootstrapper boot)
        {
            bootStrapper = boot;
        }
        public static IKernel Kernel
        {
            get { return bootStrapper.Kernel; }
        }
    }
}
