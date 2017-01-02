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
        //private StudentListModel model = new StudentListModel();

        public ActionResult Index()
        {
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            Storage s = new Storage();
            s.GetGroups();
            StudentListModel model = new StudentListModel();
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            return View(model);
        }


    }
}