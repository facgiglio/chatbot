using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace ChatBot
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "common",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/Common.js",
                    DebugPath = "~/Scripts/Common.js",
                });

            ScriptManager.ScriptResourceMapping.AddDefinition(
               "grid",
               new ScriptResourceDefinition
               {
                   Path = "~/Scripts/Grid.js",
                   DebugPath = "~/Scripts/Grid.js",
               });
        }
    }
}