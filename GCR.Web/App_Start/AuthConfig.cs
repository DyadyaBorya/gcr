using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using GCR.Web.Models;
using GCR.Core.Security;
using GCR.Core;
using Ninject;

namespace GCR.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            var provider = IoC.Kernel.Get<ISecurityProvider>();
            provider.RegisterOAuthProviders();
        }
    }
}
