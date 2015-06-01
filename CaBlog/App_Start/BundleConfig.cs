﻿using System.Web;
using System.Web.Optimization;

namespace CaBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js",
                        "~/Scripts/jquery.fittext.js",
                        "~/Scripts/jquery.mousewheel-3.0.6.pack.js",
                        "~/Scripts/jquery.easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/wow.min.js",
                      "~/Scripts/creative.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/jquery.js",
                      "~/Scripts/jquery.confirm.min.js",
                      "~/Scripts/createlinks.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/font-awesome/css/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/sb-admin.css",
                      "~/Content/creative.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                      "~/Content/bootstrap.min.css",
                      "~/font-awesome/css/font-awesome.min.css",                      
                      "~/Content/sb-admin.css"));
        }
    }
}
