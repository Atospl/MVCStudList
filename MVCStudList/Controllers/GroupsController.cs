using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCStudList.Models;
using log4net;

namespace MVCStudList
{
    public class GroupsController : Controller
    {
        private StorageContext db = new StorageContext();
        private static readonly ILog log = LogManager.GetLogger(typeof(GroupsController));

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                log.Info("Błąd przy usuwaniu grupy");
                return View("GroupError", new ErrorModel("Grupa już usunięta!"));
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDGroup,Name,Stamp")] Group group)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var groups = db.Groups.Where(gr => gr.Name.Equals(group.Name)).ToList();
                    if (groups.Count() == 0)
                        throw new Exception();
                }
                catch (Exception)
                {
                    db.Groups.Add(group);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                log.Info("Błąd przy tworzeniu grupy");
                return View("GroupError", new ErrorModel("Grupa już istnieje!"));

            }
            db = new StorageContext();
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                log.Info("Błąd przy usuwaniu grupy");
                return View("GroupError", new ErrorModel("Grupa już usunięta!"));
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            try {
                db.Groups.Remove(group);
                db.SaveChanges();
                db = new StorageContext();
            }
            catch(Exception ex)
            {
                log.Info("Błąd przy usuwaniu grupy");
                return View("GroupError", new ErrorModel("Studenci należą do grupy!"));
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
