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

            var student = model.Students.Where(stud => stud.IDStudent == int.Parse(id)).First();
            model.Index = student.IndexNo;
            model.BirthDate = student.BirthDate.ToString();
            model.BirthPlace = student.BirthPlace;
            model.LastName = student.LastName;
            model.GroupName = student.Group.Name;
            model.FirstName = student.FirstName;
            model.GroupChosen = model.Groups.Where(group => group.IDGroup == student.IDGroup).First();
            return View("StudentsList", model);
        }

        public ActionResult New(string GroupID, string FirstName, string LastName, string BirthPlace, string BirthDate, string Index)
        {
            StudentListModel model = GetModel();

            Student student = new Student(FirstName, LastName, BirthPlace, Index, DateTime.Parse(BirthDate), int.Parse(GroupID));
            try
            {
                model.CreateStudent(student);
                return View("StudentsList", model);
            }
            catch (Exception ex)
            {
                //model.ErrorMessage = "Failed to create user";
                //model.ErrorMessageHidden = false;
                //Console.WriteLine(ex.Message);
                return View("Error", new ErrorModel("Error when creating user"));
            }
        }

        public ActionResult Save(string GroupID, string FirstName, string LastName, string BirthPlace, string BirthDate, string Index)
        {
            StudentListModel model = GetModel();


            return View("StudentsList", model);

        }

        public ActionResult Remove(string GroupID, string FirstName, string LastName, string BirthPlace, string BirthDate, string Index)
        {
            StudentListModel model = GetModel();
            var student = model.GetAllStudents().Where(st => st.IndexNo.Equals(Index)).First();
            try
            {
                model.DeleteStudent(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            model = GetModel();
            return View("StudentsList", model);

        }

        public ActionResult Clear()
        {
            StudentListModel model = GetModel();
            model.CityFilter = "";
            model.GroupIDFilter = 0;
            model.Students = model.GetAllStudents(); 
            return View("Index", model);

        }

        public ActionResult Filter(string GroupID, string CityFilter)
        {
            StudentListModel model = GetModel();
            if ((CityFilter == null || CityFilter.Equals("")) && int.Parse(GroupID) == 0)
            {
                model.Students = model.GetAllStudents();
                return View("Index", model);
            }

            if ((CityFilter == null || CityFilter.Equals("")) && int.Parse(GroupID) != 0)
            {
                model.Students = model.GetAllStudents().Where(stud => stud.IDGroup == int.Parse(GroupID)).ToList();
                return View("Index", model);
            }

            model.GroupSelected = model.Groups.Where(group => group.IDGroup == int.Parse(GroupID)).First();
            var newStudents = new List<Student>();
            var students = model.GetAllStudents();
            foreach(Student student in students)
            {
                if(student.BirthPlace == null)
                    continue;
                if (int.Parse(GroupID) == 0)
                {
                    if (student.BirthPlace.ToUpper().Contains(CityFilter.ToUpper()))
                        newStudents.Add(student);
                }
                else
                {
                    if (student.BirthPlace.ToUpper().Contains(CityFilter.ToUpper()) && student.IDGroup == int.Parse(GroupID))
                        newStudents.Add(student);
                }
            }
            model.Students = newStudents;
            return View("Index", model);
        }

        private StudentListModel GetModel()
        {
            var model = stateManager.Load("model");
            if (model == null)
            {
                model = new StudentListModel();
                stateManager.Save("model", model);
            }
            //model.ErrorMessage = "";
            //model.ErrorMessageHidden = true;

            return model;
        }

    }
}