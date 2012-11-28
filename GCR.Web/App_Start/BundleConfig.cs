using System.Web;
using System.Web.Optimization;

namespace GCR.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            Bundle bun = null;

            bundles.UseCdn = true;

            // NOTE: Web Essentials bundles may only be updated at build time.
            bun = new ScriptBundle("~/Script/base", VirtualPathUtility.ToAbsolute("~/Scripts/_base.min.js")).Include(
                       "~/Scripts/jquery-{version}.js"
                       , "~/Scripts/jquery-ui-{version}.js"
                       , "~/Scripts/jquery.unobtrusive-ajax.js"
                       , "~/Scripts/jquery.validate.js"
                       , "~/Scripts/jquery.validate.unobtrusive.js"
                       , "~/Scripts/knockout-{version}.debug.js");
            bun.Transforms.Clear();
            bundles.Add(bun);

            // NOTE: Web Essentials bundles may only be updated at build time.
            bun = new StyleBundle("~/Content/css", VirtualPathUtility.ToAbsolute("~/Content/_base.min.css")).Include(
                        "~/Content/base.css",
                        "~/Content/site.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css");
            bun.Transforms.Clear();
            bundles.Add(bun);
        }
    }
}