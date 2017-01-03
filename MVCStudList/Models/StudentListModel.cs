using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Configuration;
using System.ComponentModel.DataAnnotations;

namespace MVCStudList.Models
{
    public class StudentListModel
    {
        private List<Student> students;

        public StudentListModel()
        {
            students = storage.GetStudents();
        }

        Storage storage = new Storage();
        public int GroupID { get; set; }
        public List<Group> Groups {
            get {
                var groups = new List<Group>(storage.GetGroups());
                groups.Add(new Group(""));
                return groups;
            }

            set {

            }
        }

        public List<Student> Students {
            get {
                return students;
            }
            set {
                students = value;
            }
        }

        public int GroupIDFilter;
        public Group GroupSelected;
        public string CityFilter;

        public string FirstName { get; set; }
        public string LastName;
        public string GroupName;
        public string BirthPlace;
        public string BirthDate;
        public string Index;
        public int IDStudent;
        public byte[] Stamp;
        public Student Student;

        public int PageNumber;
        public string SelectedIndexNo;

        public string GetBirthDate()
        {
            if (SelectedIndexNo == null || SelectedIndexNo.Equals(""))
                return "";
            else
                return Students.Where(stud => stud.IndexNo.Equals(SelectedIndexNo)).First().BirthDate.ToString();
        }

        public IPagedList<Student> StudentsPagedList {
            get {
                return Students.ToPagedList(PageNumber, int.Parse(WebConfigurationManager.AppSettings["StudListLen"]));
            }
        }

        public List<Student> GetAllStudents()
        {
            return storage.GetStudents();
        }

        public List<Group> GetGroups()
        {
            return storage.GetGroups();
        }

        public void CreateStudent(Student s)
        {
            storage.CreateStudent(s);
        }
        public void UpdateStudent(Student st)
        {
            storage.UpdateStudent(st);
        }
        public void DeleteStudent(Student st)
        {
            storage.DeleteStudent(st);
        }
    }
}