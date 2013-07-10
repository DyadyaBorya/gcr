using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Web.Infrastructure;
using GCR.Web.Models;

namespace GCR.Web.Controllers
{
    [HtmlAllowed]
    public class ErrorController : BaseController
    {
        public ActionResult NotFound(string url)
        {
            var originalUri = url ?? Request.QueryString["aspxerrorpath"] ?? Request.Url.OriginalString;

            var controllerName = (string)RouteData.Values["controller"];
            var actionName = (string)RouteData.Values["action"];
            var model = new HttpErrorViewModel(new HttpException(404, "Failed to find page"), controllerName, actionName)
            {
                RequestedUrl = originalUri,
                ReferrerUrl = Request.UrlReferrer == null ? "" : Request.UrlReferrer.OriginalString
            };

            Response.StatusCode = 404;
            return View("Index", model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            var controllerName = (string)RouteData.Values["controller"];
            var ex = Server.GetLastError();
            var model = new HttpErrorViewModel(ex ?? new HttpException(500, "Something went wrong."), controllerName, actionName);
            var result = new ViewResult
            {
                ViewName = "~/Views/Error/Index.cshtml",
                ViewData = new ViewDataDictionary<HttpErrorViewModel>(model),
            };


            Response.StatusCode = model.StatusCode;
            result.ExecuteResult(ControllerContext);
        }
    }
}
