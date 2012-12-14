using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using GCR.Core;

namespace GCR.Web.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString LoginView(this HtmlHelper helper)
        {
            var li = helper.ActionLink("Login", "Login", "Account");
            var lo = helper.ActionLink("Logout", "LogOff", "Account");
            return LoginView(helper, li, lo);
        }

        public static MvcHtmlString LoginView(this HtmlHelper helper, string loggedIn, string loggedOut)
        {
            return LoginView(helper, MvcHtmlString.Create(loggedIn), MvcHtmlString.Create(loggedIn));
        }

        public static MvcHtmlString LoginView(this HtmlHelper helper, MvcHtmlString loggedIn, MvcHtmlString loggedOut)
        {
            if (CurrentUser.IsAuthenticated)
            {
                return loggedOut;
            }
            else
            {
                return loggedIn;
            }
        }
    }
}