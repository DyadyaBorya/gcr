using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GCR.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("piczardWebResource.ashx/{*pathInfo}");

            routes.MapRoute(
                name: "Archive",
                url: "{controller}/Archive/{year}/{month}/{day}",
                defaults: new { controller = "News", action = "Archive", month = UrlParameter.Optional, day = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}