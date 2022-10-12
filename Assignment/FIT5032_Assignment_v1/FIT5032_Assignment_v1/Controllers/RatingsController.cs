using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_v1.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment_v1.Controllers
{
    [Authorize]
    public class RatingsController : Controller
    {
        private FIT5032_Models db = new FIT5032_Models();

        // GET: Ratings
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Dentist).Include(r => r.Patient);
            if (User.IsInRole("Admin"))
            {
                ratings = db.Ratings.Include(r => r.Dentist).Include(r => r.Patient);
            }
            if (User.IsInRole("Patient"))
            {
                var userId = User.Identity.GetUserId();
                var patient = db.Patients.Where(p => p.UserId == userId).ToList()[0];
                ratings = db.Ratings.Where(r => r.PatientId == patient.Id);
            }
            return View(ratings.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName");
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DentistId,PatientId,Score")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Patient"))
                {
                    var userId = User.Identity.GetUserId();
                    var patient = db.Patients.Where(p => p.UserId == userId).ToList()[0];
                    rating.PatientId = patient.Id;
                }
                db.Ratings.Add(rating);
                db.SaveChanges();
                double averageRating = db.Database.SqlQuery<double>("Select AVG(Score) from Ratings where DentistId = @DentistId", new SqlParameter("@DentistId", rating.DentistId)).FirstOrDefault();
                db.Database.ExecuteSqlCommand("update Dentists set AggregatedRating=@AggregatedRating where Id=@Id",
                    new SqlParameter("@Id", rating.DentistId), new SqlParameter("@AggregatedRating", averageRating));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName", rating.DentistId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName", rating.PatientId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName", rating.DentistId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName", rating.PatientId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DentistId,PatientId,Score")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Patient"))
                {
                    var userId = User.Identity.GetUserId();
                    var patient = db.Patients.Where(p => p.UserId == userId).ToList()[0];
                    rating.PatientId = patient.Id;
                }

                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                double averageRating = db.Database.SqlQuery<double>("Select AVG(Score) from Ratings where DentistId = @DentistId", new SqlParameter("@DentistId", rating.DentistId)).FirstOrDefault();
                db.Database.ExecuteSqlCommand("update Dentists set AggregatedRating=@AggregatedRating where Id=@Id",
                    new SqlParameter("@Id", rating.DentistId), new SqlParameter("@AggregatedRating", averageRating));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName", rating.DentistId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName", rating.PatientId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
            db.SaveChanges();
            double averageRating = db.Database.SqlQuery<double>("Select AVG(Score) from Ratings where DentistId = @DentistId", new SqlParameter("@DentistId", rating.DentistId)).FirstOrDefault();
            db.Database.ExecuteSqlCommand("update Dentists set AggregatedRating=@AggregatedRating where Id=@Id",
                new SqlParameter("@Id", rating.DentistId), new SqlParameter("@AggregatedRating", averageRating));
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
