using System.Web;
using System.Web.Optimization;

namespace EagleEye
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                      "~/assets/js/pace.js",
                      "~/Scripts/jquery-2.2.4.js",
                      //"~/assets/js/jquery-3.3.1.min.js",
                      "~/assets/libs/popper/popper.js",
                      "~/assets/js/bootstrap.js",
                      "~/assets/js/sidenav.js",
                      "~/assets/js/layout-helpers.js",
                      "~/assets/js/material-ripple.js",
                       "~/assets/libs/perfect-scrollbar/perfect-scrollbar.js",
                       "~/assets/libs/datatables/datatables.js",
                       "~/assets/libs/vanilla-text-mask/vanilla-text-mask.js",
                       "~/assets/libs/vanilla-text-mask/text-mask-addons.js",
                       "~/assets/libs/bootstrap-maxlength/bootstrap-maxlength.js",
                       "~/assets/js/pages/forms_extras.js",
                       "~/assets/libs/spin/spin.js",
                       "~/assets/libs/ladda/ladda.js",
                       "~/assets/libs/bootstrap-sweetalert/bootstrap-sweetalert.js",
                       "~/assets/libs/growl/growl.js",
                       "~/select.bootstrap.css",
                       "~/assets/libs/datatables/Select-1.3.1/js/dataTables.select.js",
                       "~/Scripts/jquery.signalR-2.4.1.min.js",
                       "~/assets/js/Helper.js",
                       "~/assets/libs/bootstrap-slider/bootstrap-slider.js",
                       "~/assets/libs/nouislider/nouislider.js"));

            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                    
                     "~/assets/libs/eve/eve.js",
                     "~/assets/libs/flot/flot.js",
                     "~/assets/libs/flot/curvedLines.js",
                     "~/assets/libs/chart-am4/core.js",
                     "~/assets/libs/chart-am4/charts.js",
                     "~/assets/libs/chart-am4/animated.js"));

            bundles.Add(new ScriptBundle("~/bundles/demo").Include(
                   "~/assets/js/demo.js"
                   ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/fonts/fontawesome.css",
                      "~/assets/fonts/ionicons.css",
                      "~/assets/fonts/linearicons.css",
                      "~/assets/fonts/open-iconic.css",
                      "~/assets/fonts/pe-icon-7-stroke.css",
                      "~/assets/fonts/feather.css",
                      "~/assets/css/bootstrap-material.css",
                      "~/assets/css/shreerang-material.css",
                      "~/assets/css/uikit.css",
                      "~/assets/libs/perfect-scrollbar/perfect-scrollbar.css",
                      "~/assets/libs/flot/flot.css",
                      "~/assets/libs/datatables/datatables.css",
                      "~/assets/libs/bootstrap-maxlength/bootstrap-maxlength.css",
                      "~/assets/libs/ladda/ladda.css",
                      "~/assets/libs/bootstrap-sweetalert/bootstrap-sweetalert.css",
                      "~/assets/libs/growl/growl.css",
                      "~/assets/libs/datatables/Select-1.3.1/css/select.bootstrap.css",
                      "~/assets/libs/animate-css/animate.css",
                      "~/assets/libs/bootstrap-slider/bootstrap-slider.css",
                      "~/assets/libs/nouislider/nouislider.css",
                      "~/assets/css/pages/tasks.css"));
        }
    }
}
