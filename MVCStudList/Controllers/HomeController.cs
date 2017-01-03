using MVCStudList.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Configuration;
using MVCStudList.Other;

namespace MVCStudList.Controllers
{
    public class HomeController : Controller
    {
        protected IStateManager<StudentListModel> stateManager = new SessionStateManager<StudentListModel>();

        public ActionResult Index()
        {
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            StudentListModel model = GetModel();
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            return View(model);
        }

        public ActionResult StudentsList(int? page, string currentFilter)
        {
            StudentListModel model = GetModel();
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            //TODO ładować tu studentów i filtrować


            int pageSize = 1;
            int pageNumber = (page ?? 1);
            model.PageNumber = pageNumber;
            return View(model);
        }

        public ActionResult SelectStudent(string id)
        {
            StudentListModel model = GetModel();
            model.Index = model.Students.Where(stud => stud.IndexNo.Equals(id)).First().IndexNo;
            return View("StudentsList", model);
        }

        private StudentListModel GetModel()
        {
            var model = stateManager.Load("model");
            if (model == null)
            {
                model = new StudentListModel();
                stateManager.Save("model", model);
            }
            return model;
        }
    }
}