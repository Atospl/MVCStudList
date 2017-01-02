using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Configuration;

namespace MVCStudList.Models
{
    public class StudentListModel
    {
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
                return storage.GetStudents();
            }
        }

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