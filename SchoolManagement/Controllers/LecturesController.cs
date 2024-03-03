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
    public class LecturesController : Controller
    {
        private SchoolManagementEntities db = new SchoolManagementEntities();

        // GET: Lectures
        public ActionResult Index()
        {
            var lectures = db.Lectures.Include(l => l.Batch).Include(l => l.Subject).Include(l => l.Teacher).Where(l=>l.isDeleted==false);
            return View(lectures.ToList());
        }

        // GET: Lectures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = db.Lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // GET: Lectures/Create
        public ActionResult Create()
        {
            var batches = db.Batches.ToList();
            var classDivList = batches.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = $"{b.Class} - {b.Div}"
            }).ToList();

            ViewBag.BatchId = new SelectList(classDivList, "Value", "Text");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: Lectures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,SubjectId,ScheduleDate,StartTime,Hours,BatchId")] Lecture lecture)
        {

            if (ModelState.IsValid)
            {

                lecture.isDeleted = false;
                lecture.CreatedBy = "1b87d234-e582-431a-9860-8822465102c9";
                lecture.CreatedDate = DateTime.Now;
                lecture.ModifiedBy = "1b87d234-e582-431a-9860-8822465102c9";
                lecture.ModifiedDate = DateTime.Now;
                lecture.EndTime = lecture.StartTime.Add(TimeSpan.FromHours((double)lecture.Hours)); 

                db.Lectures.Add(lecture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.BatchId = new SelectList(db.Batches, "Id", "Div", lecture.BatchId);
            //ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", lecture.SubjectId);
            //ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", lecture.TeacherId);
            return View(lecture);
        }

        // GET: Lectures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = db.Lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }

            var batches = db.Batches.ToList();
            var classDivList = batches.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = $"{b.Class} - {b.Div}"
            }).ToList();

            ViewBag.BatchId = new SelectList(classDivList, "Value", "Text");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", lecture.SubjectId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", lecture.TeacherId);
            return View(lecture);
        }

        // POST: Lectures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherId,SubjectId,ScheduleDate,StartTime,EndTime,Hours,BatchId")] Lecture lecture)
        {
            if (ModelState.IsValid)
            {
                lecture.ModifiedBy = "1b87d234-e582-431a-9860-8822465102c9";
                lecture.ModifiedDate = DateTime.Now;


                db.Lectures.Attach(lecture);
                db.Entry(lecture).Property(x => x.TeacherId).IsModified = true;
                db.Entry(lecture).Property(x => x.SubjectId).IsModified = true;
                db.Entry(lecture).Property(x => x.ScheduleDate).IsModified = true;
                db.Entry(lecture).Property(x => x.StartTime).IsModified = true;
                db.Entry(lecture).Property(x => x.EndTime).IsModified = true;
                db.Entry(lecture).Property(x => x.Hours).IsModified = true;
                db.Entry(lecture).Property(x => x.BatchId).IsModified = true;
                db.Entry(lecture).Property(x => x.ModifiedBy).IsModified = true;
                db.Entry(lecture).Property(x => x.ModifiedDate).IsModified = true;

                // Save changes to the database
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            
            ViewBag.BatchId = new SelectList(db.Batches, "Id", "Div", lecture.BatchId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", lecture.SubjectId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", lecture.TeacherId);
            return View(lecture);
        }

        // GET: Lectures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lecture lecture = db.Lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: Lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lecture lecture = db.Lectures.Find(id);
            lecture.isDeleted = true;
            db.Lectures.Attach(lecture);
            db.Entry(lecture).Property(x => x.isDeleted).IsModified = true;
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
