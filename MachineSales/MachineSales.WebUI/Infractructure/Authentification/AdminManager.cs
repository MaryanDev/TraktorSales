using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineSales.WebUI.Infractructure.Authentification
{
    public class AdminManager : UserManager<Admin>
    {
        public AdminManager(IUserStore<Admin> store)
            : base(store)
        { }

        public static AdminManager Create(IdentityFactoryOptions<AdminManager> options,
            IOwinContext context)
        {
            AdminIdentityDbContext db = context.Get<AdminIdentityDbContext>();
            AdminManager manager = new AdminManager(new UserStore<Admin>(db));
            return manager;
        }
    }
}