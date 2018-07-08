using System.Web.Optimization;

namespace pokeBbyzApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/noty/jquery.noty.js", 
                        "~/Scripts/noty/layouts/topLeft.js",
                        "~/Scripts/noty/layouts/topRight.js",
                        "~/Scripts/noty/themes/bootstrap.js",
                        "~/Scripts/noty/themes/relax.js",
                        "~/Scripts/noty/layouts/top.js",
                        "~/Scripts/noty/layouts/top.js",
                        "~/Scripts/noty/layouts/center.js",
                        "~/Scripts/noty/layouts/bottom.js",
                        "~/Scripts/noty/layouts/inline.js",
                        "~/Scripts/noty/themes/default.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/notLoggedIn").Include(
                      "~/Content/NotLoggedIn/Site.css"));

            bundles.Add(new StyleBundle("~/Content/loggedIn").Include(
                      "~/Content/LoggedIn/LoggedInStyle.css"));

            bundles.Add(new StyleBundle("~/Content/error").Include(
                      "~/Content/Error/error.css"));

        }
    }
}
