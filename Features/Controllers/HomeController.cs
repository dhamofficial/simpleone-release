using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Features.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
