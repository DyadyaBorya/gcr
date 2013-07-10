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

        internal static IKernel Kernel
        {
            get { return bootStrapper.Kernel; }
        }

        public static void SetBootstrapper(IBootstrapper boot)
        {
            bootStrapper = boot;
        }

        public static T Get<T>()
        {
            return bootStrapper.Kernel.Get<T>();
        }

        public static T Get<T>(string name)
        {
            return bootStrapper.Kernel.Get<T>(name);
        }

        public static object Get(Type service)
        {
            return bootStrapper.Kernel.Get(service);
        }

        public static object Get(Type service, string name)
        {
            return bootStrapper.Kernel.Get(service, name);
        }

        public static T TryGet<T>()
        {
            return bootStrapper.Kernel.TryGet<T>();
        }

        public static object TryGet(Type service)
        {
            return bootStrapper.Kernel.TryGet(service);
        }

        public static T TryGet<T>(string name)
        {
            return bootStrapper.Kernel.TryGet<T>(name);
        }

        public static object TryGet(Type service, string name)
        {
            return bootStrapper.Kernel.TryGet(service, name);
        }
    }
}
