using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SimpleBlog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/Admin.css"));

            bundles.Add(new StyleBundle("~/styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/Site.css"));

            bundles.Add(new StyleBundle("~/admin/scripts")
                .Include("~/Scripts/modernizr-1.7.js")
                .Include("~/Scripts/jquery-2.2.1.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/ie10-viewport-bug-workaround.js")
                .Include("~/Scripts/ie-emulation-modes-warning.js")
                .Include("~/Scripts/holder.js"));

            //bundles.Add(new StyleBundle("~/scripts")
            //    .Include("~/Scripts/modernizr-1.7.js")
            //    .Include("~/Scripts/jquery-2.2.1.js")
            //    .Include("~/Scripts/jquery.validate.js")
            //    .Include("~/Scripts/jquery.validate.unobtrusive.js")
            //    .Include("~/Scripts/bootstrap.js")
            //    .Include("~/Scripts/ie10-viewport-bug-workaround.js")
            //    .Include("~/Scripts/ie-emulation-modes-warning.js")
            //    .Include("~/Scripts/holder.js"));
        }
    }
}