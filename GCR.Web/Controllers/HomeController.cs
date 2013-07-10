using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Web.Infrastructure;

namespace GCR.Web.Controllers
{
    [HtmlAllowed]
    public class HomeController : BaseController
    {
        public HomeController()
        {
            SetActiveTab(Tabs.Home);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            SetActiveTab(Tabs.About);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
