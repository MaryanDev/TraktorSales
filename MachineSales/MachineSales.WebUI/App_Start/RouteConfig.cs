using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MachineSales.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Content", action = "Home", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "admin/",
            //    defaults: new { controller = "Admin", action = "Dashboard" }
            //);
        }
    }
}
