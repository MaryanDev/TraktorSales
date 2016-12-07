using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineSales.WebUI.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Login()
        {
            return View();
        }
    }
}