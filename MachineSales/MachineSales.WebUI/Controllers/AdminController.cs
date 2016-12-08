﻿using MachineSales.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineSales.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private EFRepository _repository;
        public AdminController()
        {
            _repository = new EFRepository();
        }
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}