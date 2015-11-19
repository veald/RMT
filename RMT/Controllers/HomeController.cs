using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Vous avez un projet? n'hesitez pas à me contacter.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Vous avez un projet? N'hesitez pas à me contacter.";

            return View();
        }
    }
}