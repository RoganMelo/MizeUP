using System.Web;
using System.Web.Optimization;

namespace MizeUP.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/site.css", 
                "~/Content/css/bootstrap-clockpicker.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery.navgoco.js", 
                "~/Scripts/bootstrap.min.js", 
                "~/Scripts/site.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
