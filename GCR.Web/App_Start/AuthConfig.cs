using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using GCR.Web.Models;

namespace GCR.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "000000004C0DE076",
                clientSecret: "5xap8kbNRP-j1rtdu2F6AJtuMzXzue3S");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
