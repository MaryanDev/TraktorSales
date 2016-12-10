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
        public ActionResult Dashboard()
        {
            var machines = _repository.Get<Machine>();
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
            //var images = _repository.Get<Image>(i => i.MachineId == UpdatedMachine.Id);
            //foreach(var image in images)
            //{
            //    _repository.Delete(image);
            //}

            //foreach(var newImage in machineToUpdate.Images)
            //{
            //    _repository.Insert(new Image { ImagePath = newImage.ImagePath });
            //}
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
                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    file.InputStream.CopyTo(ms);
                //    byte[] array = ms.GetBuffer();
                //}

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

            if (System.IO.File.Exists(mainImgPath))
            {
                System.IO.File.Delete(mainImgPath);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult DeleteSecondaryImage(int imageId)
        {
            var imageToDelete = _repository.GetSingle<Image>(i => i.Id == imageId);
            var secondaryImagePath = Server.MapPath(imageToDelete.ImagePath);
            _repository.Delete<Image>(imageToDelete);

            if (System.IO.File.Exists(secondaryImagePath))
            {
                System.IO.File.Delete(secondaryImagePath);
            }
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
                _repository.Delete(machineToDelete);
                if (System.IO.File.Exists(mainImageToDelete))
                {
                    System.IO.File.Delete(mainImageToDelete);
                }

                var secondaryImagesToDelete =
                    _repository.Get<Image>(i => i.MachineId == machineToDelete.Id)
                        .Select(i => Server.MapPath(i.ImagePath))
                        .ToList();

                foreach (var image in secondaryImagesToDelete)
                {
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
        }
    }
}