using MachineSales.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineSales.WebUI.Controllers
{
    public class ContentController : Controller
    {
        private EFRepository _repository;
        public ContentController()
        {
            _repository = new EFRepository();
        }
        // GET: Content
        public ActionResult Home()
        {
            return View();
        }
    }
}