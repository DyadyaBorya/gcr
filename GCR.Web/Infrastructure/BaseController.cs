using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCR.Web
{
    public class BaseController : Controller
    {
        public void SetActiveTab(Tabs activeTab)
        {
            ViewBag.HomeTab = null;
            ViewBag.AboutTab = null;
            ViewBag.ScheduleTab = null;
            ViewBag.MemberTab = null;
            ViewBag.NewsTab = null;

            switch (activeTab)
            {
                case Tabs.Home:
                    ViewBag.HomeTab = "on";
                    break;
                case Tabs.About:
                    ViewBag.AboutTab = "on";
                    break;
                case Tabs.Schedule:
                    ViewBag.ScheduleTab = "on";
                    break;
                case Tabs.Member:
                    ViewBag.MemberTab = "on";
                    break;
                case Tabs.News:
                    ViewBag.NewsTab = "on";
                    break;
            }
        }
    }
}
