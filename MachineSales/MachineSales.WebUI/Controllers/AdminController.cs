using MachineSales.WebUI.Entities;
using MachineSales.WebUI.Repositories;
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
            var machines = _repository.Get<Machine>();
            return View(machines);
        }

        [HttpGet]
        public JsonResult GetMachineInfo(int id)
        {
            var machineInfo = _repository.Get<Machine>(m => m.Id == id).Select(m => new
            {
                Id = m.Id,
                Price = m.Price,
                Description = m.Description,
                Model = m.Model,
                MainImage = m.MainImage,
                Images = m.Images.Select(i => i.ImagePath)
            });

            return Json(machineInfo, JsonRequestBehavior.AllowGet);
        }
    }
}