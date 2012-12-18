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
                       , "~/Scripts/json2.js"
                       , "~/Scripts/charCount.js"
                       , "~/Scripts/Common.js");
            bun.Transforms.Clear();
            bundles.Add(bun);

            // NOTE: Web Essentials bundles may only be updated at build time.
            bun = new StyleBundle("~/Content/css", VirtualPathUtility.ToAbsolute("~/Content/_base.min.css")).Include(
                        "~/Content/base.css",
                        "~/Content/site.css");
            bun.Transforms.Clear();
            bundles.Add(bun);

            string theme = "custom";
            bun = new StyleBundle("~/Content/themes/css", VirtualPathUtility.ToAbsolute("~/Content/themes/" + theme + "/minified/jquery-ui.min.css")).Include(
            "~/Content/themes/" + theme + "/jquery.ui.core.css",
            "~/Content/themes/" + theme + "/jquery.ui.accordion.css",
            "~/Content/themes/" + theme + "/jquery.ui.autocomplete.css",
            "~/Content/themes/" + theme + "/jquery.ui.button.css",
            "~/Content/themes/" + theme + "/jquery.ui.datepicker.css",
            "~/Content/themes/" + theme + "/jquery.ui.dialog.css",
            "~/Content/themes/" + theme + "/jquery.ui.menu.css",
            "~/Content/themes/" + theme + "/jquery.ui.progressbar.css",
            "~/Content/themes/" + theme + "/jquery.ui.resizable.css",
            "~/Content/themes/" + theme + "/jquery.ui.selectable.css",
            "~/Content/themes/" + theme + "/jquery.ui.slider.css",
            "~/Content/themes/" + theme + "/jquery.ui.spinner.css",
            "~/Content/themes/" + theme + "/jquery.ui.tabs.css",
            "~/Content/themes/" + theme + "/jquery.ui.tooltip.css",
            "~/Content/themes/" + theme + "/jquery.ui.theme.css");
            bun.Transforms.Clear();
            bundles.Add(bun);
        }
    }
}