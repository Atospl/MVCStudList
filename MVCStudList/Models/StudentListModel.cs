using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public virtual List<Student> GetStudents()
        {
            return storage.GetStudents();
        }

        //public List<Group> GetGroupsForListing()
        //{

        //}

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