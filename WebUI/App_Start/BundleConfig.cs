﻿using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Create bundel for jQueryUI  
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-{version}.js"));
 
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/jCaneCodeUi").Include(
                      "~/Scripts/canecode_switch.js"));

            bundles.Add(new StyleBundle("~/Content/cssCaneCodeUi").Include(
                   "~/Content/themes/base/canecode_switch.css"));
            

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Add Materialize Scripts

            //bundles.Add(new ScriptBundle("~/bundles/materializejs").Include(
            //           "~/Scripts/Materialize/admin.js",
            //           "~/Scripts/Materialize/jqvmap/*.js",
            //           "~/Scripts/Materialize/page-scripts/*.js"
            //           ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css",
            //          "~/Content/themes/Materialize/*.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/themes/Materialize/admin-materialize.min.css",
                      "~/Content/themes/Materialize/jqvmap.css",
                      "~/Content/themes/Materialize/materialize.min.css",
                      "~/Content/themes/Materialize/flag-icon.min.css",
                      "~/Content/themes/Site.css"));

        }
    }
}
