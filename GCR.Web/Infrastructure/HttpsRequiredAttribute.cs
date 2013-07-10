using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GCR.Core;

namespace GCR.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class HttpsRequiredAttribute : RequireHttpsAttribute
    {
        protected virtual bool AuthorizeInternal(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            // Check the request
            if (httpContext.Request.IsSecureConnection ||
               (httpContext.Request.ServerVariables["HTTPS"] != null &&
                httpContext.Request.ServerVariables["HTTPS"].ToLower() == "on"))
            {
                return true;
            }
            if ((!string.IsNullOrEmpty(httpContext.Request.ServerVariables["HTTP_HOST"].ToString())) &&
                (httpContext.Request.ServerVariables["HTTP_HOST"].ToString().EndsWith("443")))
            {
                return true;
            }
            // url is not secure:
            return false;
        }

        //protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        //{
        //    if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
        //    {
        //        throw new InvalidOperationException("The requested resource can only be accessed via SSL.");
        //    }
        //    string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
        //    filterContext.Result = new RedirectResult(url);
        //}


        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = this.OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            bool inherit = true;
            if (!filterContext.IsChildAction &&
                !filterContext.ActionDescriptor.IsDefined(typeof(HtmlAllowedAttribute), inherit) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(HtmlAllowedAttribute), true))
            {
                if (this.AuthorizeInternal(filterContext.HttpContext))
                {
                    HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                    cache.SetProxyMaxAge(new TimeSpan(0L));
                    cache.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler), null);
                }
                else
                {
                    this.HandleNonHttpsRequest(filterContext);
                }
            }
        }

        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (!this.AuthorizeInternal(httpContext))
            {
                return HttpValidationStatus.IgnoreThisRequest;
            }
            return HttpValidationStatus.Valid;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class HtmlAllowedAttribute : Attribute
    {
    }
}