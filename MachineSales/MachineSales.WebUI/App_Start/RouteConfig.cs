﻿using System;
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
<<<<<<< HEAD
            routes.MapMvcAttributeRoutes();
//=======
//>>>>>>> 3bc9d2f66f8f3fee64c703b21da8ca4e70142f78
=======
//<<<<<<< HEAD
//<<<<<<< HEAD
            routes.MapMvcAttributeRoutes();
//=======
//>>>>>>> 3bc9d2f66f8f3fee64c703b21da8ca4e70142f78
//=======
            routes.MapMvcAttributeRoutes();

//>>>>>>> parent of 60e25ae... merge needed
//=======
//>>>>>>> 7b9e4261c4aa7001341f0c3210ff6e7bec417fff
>>>>>>> deaa69b95158ee42f5acf17b70041dce17c804a3
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
