using MVCStudList.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStudList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}