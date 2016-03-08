using System.Web;
using System.Web.Optimization;

namespace Features
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-1.10.2.min.js"
                , "~/Scripts/angular.min.js"
                , "~/Scripts/angular-route.min.js"
                , "~/Scripts/angular-messages.min.js"
                , "~/Scripts/angular-animate.min.js"
                , "~/Scripts/angular-sanitize.js"
                , "~/Scripts/toaster.min.js"
                , "~/Scripts/bootstrap.min.js"
                , "~/Scripts/angular-local-storage.js"
                , "~/Scripts/ng-tags-input.min.js"
                , "~/Scripts/app.nginit.js"
                , "~/Scripts/app.factory.js"
                , "~/Scripts/app.directives.js"
                , "~/Scripts/app.controller.js"
                , "~/Scripts/app.main.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.min.css",
                 "~/Content/ng-tags-input.min.css",
                 "~/Content/Site.css"
                 ));

            BundleTable.EnableOptimizations = true;
            bundles.FileSetOrderList.Clear();
        }
    }
}
