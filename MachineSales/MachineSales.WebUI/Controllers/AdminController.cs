using MachineSales.WebUI.Entities;
using MachineSales.WebUI.Entities.DTOs;
using MachineSales.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Dashboard(int page = 1)
        {
            var machines = _repository.Get<Machine>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList(); 
            var count = GetCountOfPages(_repository.Get<Machine>().Count, pageSize);
            ViewBag.allPages = count;
            return View(machines);
        }

        [HttpGet]
        public JsonResult GetMachineInfo(int id)
        {
            var machineInfo = _repository.Get<Machine>(m => m.Id == id).Select(m => new FullMachineInfoDto
            {
                Id = m.Id,
                Description = m.Description,
                Price = m.Price,
                Model = m.Model,
                MainImage = m.MainImage,
                Images = m.Images.Select(i => new Image { Id = i.Id, ImagePath = i.ImagePath }).ToList()
            }).FirstOrDefault();

            return Json(machineInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateMachine(FullMachineInfoDto machineToUpdate)
        {
            var updatedMachine = _repository.Update<Machine>(new Entities.Machine
            {
                Id = machineToUpdate.Id,
                Model = machineToUpdate.Model,
                Price = machineToUpdate.Price,
                Description = machineToUpdate.Description,
                MainImage = machineToUpdate.MainImage ?? ""
            });
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult ModifyImages(HttpPostedFileBase file, int machineId)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("/Content/Images"), pic);
                // file is uploaded
                file.SaveAs(path);

                _repository.Insert<Image>(new Entities.Image { ImagePath = Path.Combine("/Content/Images", pic), MachineId = machineId });
            }
            // after successfully uploading redirect the user
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult ModifyMainImage(HttpPostedFileBase file, int machineId)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("/Content/Images"), pic);
                // file is uploaded
                file.SaveAs(path);

                var machineToUpdate = _repository.GetSingle<Machine>(m => m.Id == machineId);
                machineToUpdate.MainImage = Path.Combine("/Content/Images", pic);
                _repository.Update<Machine>(machineToUpdate);
            }
            // after successfully uploading redirect the user
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult DeleteMainImage(int machineId)
        {
            var machineToUpdate = _repository.GetSingle<Machine>(m => m.Id == machineId);
            var mainImgPath = Server.MapPath(machineToUpdate.MainImage);
            machineToUpdate.MainImage = "";
            _repository.Update(machineToUpdate);

            DeleteImage(mainImgPath);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult DeleteSecondaryImage(int imageId)
        {
            var imageToDelete = _repository.GetSingle<Image>(i => i.Id == imageId);
            var secondaryImagePath = Server.MapPath(imageToDelete.ImagePath);
            _repository.Delete<Image>(imageToDelete);

            DeleteImage(secondaryImagePath);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public JsonResult CreateMachine(FullMachineInfoDto machineToCreate)
        {
            var createdMachine = _repository.Insert<Machine>(new Machine
            {
                Model = machineToCreate.Model,
                Description = machineToCreate.Description,
                Price = machineToCreate.Price,
                MainImage = ""
            });

            return Json(createdMachine.Id);
        }

        [HttpPost]
        public ActionResult DeleteMachine(int machineId)
        {
            var machineToDelete = _repository.GetSingle<Machine>(m => m.Id == machineId);
            if (machineToDelete != null)
            {
                var mainImageToDelete = Server.MapPath(machineToDelete.MainImage);
                var secondaryImagesToDelete =
                    _repository.Get<Image>(i => i.MachineId == machineToDelete.Id);

                foreach (var img in secondaryImagesToDelete)
                {
                    _repository.Delete(img);
                }
                var secondaryImagesPathToDelete = secondaryImagesToDelete.Select(i => Server.MapPath(i.ImagePath));
                foreach (var image in secondaryImagesPathToDelete)
                {
                    DeleteImage(image);
                }
                _repository.Delete(machineToDelete);
                DeleteImage(mainImageToDelete);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }

        protected int pageSize = 5;
        protected int GetCountOfPages(int allPages, int size)
        {
            var pages = allPages / size;
            var count = allPages % size == 0 ? pages : ++pages;
            return count;
        }

        private void DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}