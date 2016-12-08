using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineSales.WebUI.Infractructure.Authentification
{
    public class AdminIdentityDbContext : IdentityDbContext<Admin>
    {
        public AdminIdentityDbContext() : base("name=IdentityDb") { }


        public static AdminIdentityDbContext Create()
        {
            return new AdminIdentityDbContext();
        }
    }
}