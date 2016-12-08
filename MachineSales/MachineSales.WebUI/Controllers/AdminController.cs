using MachineSales.WebUI.Entities;
using MachineSales.WebUI.Entities.DTOs;
using MachineSales.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            }).FirstOrDefault();

            return Json(machineInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateMachine(FullMachineInfoDto machineToUpdate)
        {
            var UpdatedMachine = _repository.Update<Machine>(new Entities.Machine
            {
                Id = machineToUpdate.Id,
                Model = machineToUpdate.Model,
                Price = machineToUpdate.Price,
                Description = machineToUpdate.Description
            });
            var images = _repository.Get<Image>(i => i.MachineId == UpdatedMachine.Id);
            foreach(var image in images)
            {
                _repository.Delete(image);
            }

            foreach(var newImage in machineToUpdate.Images)
            {
                _repository.Insert(new Image { ImagePath = newImage });
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}