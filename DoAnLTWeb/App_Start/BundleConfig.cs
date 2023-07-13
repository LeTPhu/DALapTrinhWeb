using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace DoAnLTWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //--Css bundle for page--//
            bundles.Add(new StyleBundle("~/bundles/css1").Include("~/Content/bootstrap.min.css",
                                                                 "~/Content/font-awesome.min.css",
                                                                 "~/Content/elegant-icons.css",
                                                                 "~/Content/nice-select.css",
                                                                 "~/Content/jquery-ui.min.css",
                                                                 "~/Content/owl.carousel.min.css",
                                                                 "~/Content/slicknav.min.css",
                                                                 "~/Content/style.css",
																 "~/Content/main2.css",
																  //"~/Content/main3.css",
																 "~/Content/PagedList.css"));
            //--Script bundel for page--/
            bundles.Add(new ScriptBundle("~/bundle/Scripts1").Include("~/Scripts/jquery-3.4.1.min.js",
                                                                      "~/Scripts/bootstrap.min.js",
                                                                      "~/Scripts/jquery.nice-select.min.js",
                                                                      "~/Scripts/jquery-ui.min.js",
                                                                      "~/Scripts/jquery.slicknav.js",
                                                                      "~/Scripts/mixitup.min.js",
                                                                      "~/Scripts/owl.carousel.min.js",
                                                                      "~/Scripts/main.js",
                                                                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            //--Css bundle for admin--//
            bundles.Add(new StyleBundle("~/bundles/css2").Include("~/Content/bootstrap.min.css",
                                                                  "~/Content/font-awesome.min.css",
                                                                  "~/Content/elegant-icons.css",
																  "~/Content/style.css",
																  "~/Content/nice-select.css",
                                                                  "~/Content/jquery-ui.min.css",
                                                                  "~/Content/owl.carousel.min.css",
                                                                  "~/Content/slicknav.min.css",
                                                                  "~/Content/bootstrap.css",
                                                                  "~/Content/font-awesome.min.css",
                                                                  "~/Content/themify-icons.css",
                                                                  "~/Content/jquery-jvectormap-2.0.3.css",
                                                                  "~/Content/bootstrap-markdown.min.css",
                                                                  "~/Content/main.css",
                                                                  //"~/Content/ie7.css",
																  "~/Content/datatable/css/datatable.css.css",
																  "~/Content/datatable/css/datatable-bootstrap.css",
																  "~/Content/main2.css",
																  "~/Content/main3.css",
																  "~/Content/PagedList.css"));
            //--Script bundle fo admin--//
            bundles.Add(new ScriptBundle("~/bundle/Scripts2").Include("~/Scripts/jquery.min.js",
                                                                      "~/Scripts/popper.js",
                                                                      "~/Scripts/bootstrap.js",
                                                                      "~/Scripts/metisMenu.min.js",
                                                                      "~/Scripts/jquery.slimscroll.min.js",
                                                                      "~/Scripts/bootstrap-markdown.js",
                                                                      "~/Scripts/Chart.min.js",
                                                                      "~/Scripts/jquery-jvectormap-2.0.3.min.js",
                                                                      "~/Scripts/jquery-jvectormap-world-mill-en.js",
                                                                      "~/Scripts/jquery-jvectormap-us-aea-en.js",
																	  "~/Scripts/app.js",
																	  //"~/Scripts/ie7.js",
																	  "~/Content/datatable/js/datatable.min.js",
																	  "~/Content/datatable/js/datatable.jquery.min.js",
																	  "~/Scripts/dashboard_1_demo.js",
                                                                      "~/Scripts/main.js",
                                                                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));
        }


    }
}
