using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace MVCStudList.Models
{
    public class Storage
    {
        public virtual List<Student> GetStudents()
        {
            using (var db = new StorageContext())
            {
                //db.Groups.Load();
                //db.Students.Include(p => p.Group);
                return db.Students.Include(p => p.Group).ToList();
            }
        }

        public List<Group> GetGroups()
        {
            using (var db = new StorageContext())
            {
                db.Groups.Load();
                var groups = db.Groups.ToList();
                return db.Groups.ToList();
            }
        }

        public void CreateStudent(Student s)
        {
            using (var db = new StorageContext())
            {
                var group = db.Groups.Find(s.IDGroup);
                db.Students.Add(s);
                db.SaveChanges();
            }
        }

        public void UpdateStudent(Student st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Students.Find(st.IDStudent);
                if (original != null)
                {
                    original.FirstName = st.FirstName;
                    original.LastName = st.LastName;
                    original.IDGroup = st.IDGroup;
                    original.BirthDate = st.BirthDate;
                    original.BirthPlace = st.BirthPlace;
                    original.IndexNo = st.IndexNo;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteStudent(Student st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Students.Find(st.IDStudent);
                if (original != null)
                {
                    db.Students.Remove(original);
                    db.SaveChanges();
                }
            }
        }


    }
}