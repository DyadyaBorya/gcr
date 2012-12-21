using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace GCR.Web.Infrastructure
{
    public static class HttpStatusDescription
    {
        private static MethodInfo getDescriptionMethod;

        static HttpStatusDescription()
        {
            getDescriptionMethod = typeof(HttpStatusCode).Assembly.
                GetType("System.Net.HttpStatusDescription").
                GetMethod("Get", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(int) }, null);

        }

        public static string Get(int code)
        {
            return getDescriptionMethod.Invoke(null, new object[] {code}).ToString();
        }
    }
}