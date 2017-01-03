using MachineSales.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineSales.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected EFRepository _repository;

        public BaseController()
        {
            _repository = new EFRepository();
            pageSize = 5;
        }

        protected virtual int pageSize { get; set; }

        protected int GetCountOfPages(int allPages, int size)
        {
            var pages = allPages / size;
            var count = allPages % size == 0 ? pages : ++pages;
            return count;
        }
    }
}