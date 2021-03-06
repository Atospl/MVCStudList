﻿using MVCStudList.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Configuration;
using MVCStudList.Other;
using log4net;

namespace MVCStudList.Controllers
{
    public class HomeController : Controller
    {
        protected IStateManager<StudentListModel> stateManager = new SessionStateManager<StudentListModel>();
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));


        public ActionResult Index()
        {
            Database.SetInitializer<StorageContext>(null);  // to wyłącza sprawdzanie migracji
            StudentListModel model = GetModel();
            model.Students = model.GetAllStudents();
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

            model.Students = model.GetAllStudents();
            return View("StudentsList", model);
        }

        public ActionResult New(string GroupID, string FirstName, string LastName, string BirthPlace, string BirthDate, string Index)
        {
            StudentListModel model = GetModel();
            if (Index.Equals(""))
                return View("StudentsList", model);

            Student student = new Student(FirstName, LastName, BirthPlace, Index, DateTime.Parse(BirthDate), int.Parse(GroupID));
            //student.Group = model.Groups.Where(group => group.IDGroup == int.Parse(GroupID)).First();
            try
            {
                model.CreateStudent(student);
                model.Students = model.GetAllStudents();
                return View("StudentsList", model);
            }
            catch (Exception ex)
            {
                //model.ErrorMessage = "Failed to create user";
                //model.ErrorMessageHidden = false;
                //Console.WriteLine(ex.Message);
                log.Info("Błąd przy tworzeniu użytkownika");
                return View("Error", new ErrorModel("Błąd przy tworzeniu użytkownika!"));
            }
        }

        public ActionResult Save(string GroupID, string FirstName, string LastName, string BirthPlace, string BirthDate, string Index)
        {
            StudentListModel model = GetModel();
            var oldStudent = model.Students.Where(student => student.IndexNo.Equals(Index)).First();
            var newStudent = model.GetAllStudents().Where(student => student.IndexNo.Equals(Index)).First();
            if (!oldStudent.Stamp.SequenceEqual(newStudent.Stamp))
            {
                model.Students = model.GetAllStudents();
                log.Info("Błąd przy modyfikowaniu użytkownika");
   
                return View("Error", new ErrorModel("Student uprzednio zmodyfikowany!"));
            }
            else
            {
                oldStudent.FirstName = FirstName;
                oldStudent.LastName = LastName;
                oldStudent.BirthPlace = BirthPlace;
                oldStudent.BirthDate = DateTime.Parse(BirthDate);
                oldStudent.IndexNo = Index;
                oldStudent.IDGroup = int.Parse(GroupID);
                model.UpdateStudent(oldStudent);
            }

            model.Students = model.GetAllStudents();
            model.FirstName = "";

            return View("StudentsList", model);

        }

        public ActionResult Remove(string GroupID, string FirstName, string LastName, string BirthPlace, string BirthDate, string Index)
        {
            StudentListModel model = GetModel();
            var oldStudent = model.Students.Where(student => student.IndexNo.Equals(Index)).First();
            var newStudent = model.GetAllStudents().Where(student => student.IndexNo.Equals(Index)).First();
            try
            {
                if (!oldStudent.Stamp.SequenceEqual(newStudent.Stamp))
                    throw new Exception();
                model.DeleteStudent(oldStudent);
            }
            catch (Exception ex)
            {
                model.Students = model.GetAllStudents();
                log.Info("Błąd przy modyfikowaniu użytkownika");
                return View("Error", new ErrorModel("Student uprzednio zmodyfikowany!"));
            }
            model.Students = model.GetAllStudents();
            model.FirstName = "";
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