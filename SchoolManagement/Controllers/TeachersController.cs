using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Data;

namespace SchoolManagement.Controllers
{
    public class TeachersController : Controller
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,isDeleted,AddeddBy,AddedDate,ModifiedBy,ModifiedDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                //temporarily hardcoding the userid as there is only one user
                teacher.isDeleted = false;
                teacher.AddedBy = "1b87d234-e582-431a-9860-8822465102c9";
                teacher.AddedDate = DateTime.Now;
                teacher.ModifiedBy = "1b87d234-e582-431a-9860-8822465102c9";
                teacher.ModifiedDate = DateTime.Now;

                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                // Update the modified fields
                teacher.ModifiedBy = "1b87d234-e582-431a-9860-8822465102c9";
                teacher.ModifiedDate = DateTime.Now;

                // Attach the entity and mark the specified properties as modified
                db.Teachers.Attach(teacher);
                db.Entry(teacher).Property(x => x.Name).IsModified = true;
                db.Entry(teacher).Property(x => x.ModifiedBy).IsModified = true;
                db.Entry(teacher).Property(x => x.ModifiedDate).IsModified = true;

                // Save changes to the database
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(teacher);
        }



        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
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
