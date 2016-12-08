using MachineSales.WebUI.Infractructure.Authentification;
using MachineSales.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MachineSales.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            Admin user = UserManager.Find(loginModel.Login, loginModel.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Неправильне ім'я або пароль.");
            }
            else
            {
                ClaimsIdentity ident = UserManager.CreateIdentity(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            return View(loginModel);
        }

        private AdminManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AdminManager>();
            }
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}