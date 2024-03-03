using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Data;

namespace SchoolManagement.Controllers
{
    public class BatchesController : Controller
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: Batches
        public ActionResult Index()
        {
            var batches = db.Batches.Include(b => b.Teacher).Where(l=>l.isDeleted==false);
            return View(batches.ToList());
        }

        // GET: Batches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // GET: Batches/Create
        public ActionResult Create()
        {
            ViewBag.ClassTeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: Batches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassTeacherId,Class,Div")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                batch.isDeleted = false;
                batch.CreatedBy = "1b87d234-e582-431a-9860-8822465102c9";
                batch.CreatedDate = DateTime.Now;
                batch.ModifiedBy = "1b87d234-e582-431a-9860-8822465102c9";
                batch.ModifiedDate = DateTime.Now;

                db.Batches.Add(batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassTeacherId = new SelectList(db.Teachers, "Id", "Name", batch.ClassTeacherId);
            return View(batch);
        }

        // GET: Batches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassTeacherId = new SelectList(db.Teachers, "Id", "Name", batch.ClassTeacherId);
            return View(batch);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassTeacherId,Class,Div")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                batch.isDeleted = false;
                batch.CreatedBy = "1b87d234-e582-431a-9860-8822465102c9";
                batch.CreatedDate = DateTime.Now;
                batch.ModifiedBy = "1b87d234-e582-431a-9860-8822465102c9";
                batch.ModifiedDate = DateTime.Now;

                db.Batches.Add(batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassTeacherId = new SelectList(db.Teachers, "Id", "Name", batch.ClassTeacherId);
            return View(batch);
        }

        // GET: Batches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batch batch = db.Batches.Find(id);
            batch.isDeleted = true;

            db.Batches.Attach(batch);
            db.Entry(batch).Property(x => x.isDeleted).IsModified = true;
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
