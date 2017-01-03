using MachineSales.WebUI.Entities;
using MachineSales.WebUI.Entities.DTOs;
using MachineSales.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineSales.WebUI.Controllers
{
    public class ContentController : BaseController
    {
        public ContentController() : base()
        {
            this.pageSize = 6;
        }
        // GET: Content
        public ActionResult Home()
        {
            return View();  
        }

        protected override int pageSize { get; set; }

        public ActionResult Sales(int page = 1)
        {
            var machines = _repository.Get<Machine>().Select(m => new Machine
            {
                Id = m.Id,
                MainImage = m.MainImage,
                Model = m.Model,
                Price = m.Price
            })
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            var count = GetCountOfPages(_repository.Get<Machine>().Count, pageSize);
            ViewBag.allPages = count;

            return View(machines);
        }

        public ActionResult MachineDetails(int id)
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

            return View(machineInfo);
        }
    }
}