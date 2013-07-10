using System.Web;
using System.Web.Mvc;
using GCR.Core;
using GCR.Web.Infrastructure;

namespace GCR.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            if (Configuration.RequireSSL)
            {
                filters.Add(new HttpsRequiredAttribute());
            }
        }
    }
}