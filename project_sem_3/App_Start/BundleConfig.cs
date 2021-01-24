using System.Web;
using System.Web.Optimization;

namespace project_sem_3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // admin js
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/Scripts/jquery.easing.{version}.js",
                        "~/Scripts/ruang-admin.min.js",
                        "~/Scripts/Chart.min.js"));

            // admin table js
            bundles.Add(new ScriptBundle("~/bundles/admin-table").Include(
                        "~/Content/datatables/jquery.dataTables.min.js",
                        "~/Content/datatables/dataTables.bootstrap4.min.js"));

            // admin demo chart js
            bundles.Add(new ScriptBundle("~/bundles/chart-demo").Include(
                        "~/Scripts/demo/chart-area-demo.js",
                        "~/Scripts/demo/chart-bar-demo.js",
                        "~/Scripts/demo/chart-pie-demo.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        "~/Scripts/umd/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/client").Include(
                      //"~/Scripts/client/jquery.min.js",
                      "~/Scripts/client/jquery-migrate-3.0.1.min.js",
                      //"~/Scripts/client/popper.min.js",
                      //"~/Scripts/client/bootstrap.min.js",
                      "~/Scripts/client/jquery.easing.1.3.js",
                      "~/Scripts/client/jquery.waypoints.min.js",
                      "~/Scripts/client/jquery.stellar.min.js",
                      "~/Scripts/client/owl.carousel.min.js",
                      "~/Scripts/client/jquery.magnific-popup.min.js",
                      "~/Scripts/client/aos.js",
                      "~/Scripts/client/jquery.animateNumber.min.js",
                      "~/Scripts/client/bootstrap-datepicker.js",
                      "~/Scripts/client/scrollax.min.js",
                      "~/Scripts/client/google-map.js",
                      "~/Scripts/client/main.js"
                      ));

            // client css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     //"~/Content/fontawesome-free/css/all.min.css",
                     //"~/Content/bootstrap.css",
                     "~/Content/css/open-iconic-bootstrap.min.css",
                     "~/Content/css/animate.css",
                     "~/Content/css/owl.carousel.min.css",
                     "~/Content/css/owl.theme.default.min.css",
                     "~/Content/css/magnific-popup.css",
                     "~/Content/css/aos.css",
                     "~/Content/css/ionicons.min.css",
                     "~/Content/css/bootstrap-datepicker.css",
                     "~/Content/css/jquery.timepicker.css",
                     "~/Content/css/flaticon.css",
                     "~/Content/css/icomoon.css",
                     "~/Content/css/style.css",
                     "~/Content/Site.css"));


            // admin css
            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                        "~/Content/fontawesome-free/css/all.min.css",
                        "~/Content/bootstrap.css",
                        "~/Content/ruang-admin.css",
                        "~/Content/admin.css"));

            // admin css
            bundles.Add(new StyleBundle("~/Content/admin-table-css").Include(
                      "~/Content/datatables/dataTables.bootstrap4.min.css"));
        }
    }
}
