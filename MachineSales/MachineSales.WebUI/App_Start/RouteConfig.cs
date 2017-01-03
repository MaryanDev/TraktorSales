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

//<<<<<<< HEAD
            routes.MapMvcAttributeRoutes();
//=======
//>>>>>>> 3bc9d2f66f8f3fee64c703b21da8ca4e70142f78
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Content", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
