using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core;
using GCR.Web.Models;
using Newtonsoft.Json;

namespace GCR.Web.Infrastructure
{
    public static class ControllerExtensions
    {
        public static void ShowMessageAfterRedirect(this Controller controller, MessageMode status, string message)
        {
            controller.TempData["StatusMessage"] = JsonConvert.SerializeObject(new StatusMessage(status, message));
        }
        public static void ShowMessageAfterRedirect(this Controller controller, StatusMessage message)
        {
            controller.TempData["StatusMessage"] = JsonConvert.SerializeObject(message);
        }
    }
}