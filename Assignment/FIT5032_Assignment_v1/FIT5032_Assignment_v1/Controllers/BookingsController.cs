using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_v1.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment_v1.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private FIT5032_Models db = new FIT5032_Models();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Dentist).Include(b => b.Location).Include(b => b.Patient);
            if (User.IsInRole("Admin") || User.IsInRole("Receptionist"))
            {
                bookings = db.Bookings.Include(b => b.Dentist).Include(b => b.Location).Include(b => b.Patient);

            }
            if (User.IsInRole("Patient"))
            {
                var userId = User.Identity.GetUserId();
                var patient = db.Patients.Where(p => p.UserId == userId).ToList()[0];
                bookings = db.Bookings.Where(b => b.PatientId == patient.Id);
            }
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName");
            
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,StartDateTime,EndDateTime,LocationId,DentistId,PatientId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var timeIsVaild = true;
                var bookingList = db.Bookings.SqlQuery("Select * from Bookings").ToList<Booking>();
                //checking the starttime or endtime is not in between appointments of the same dentist
                foreach (Booking oldBooking in bookingList)
                {
                    if (oldBooking.DentistId == booking.DentistId && ((DateTime.Compare(oldBooking.StartDateTime, booking.StartDateTime) < 0 && DateTime.Compare(oldBooking.EndDateTime, booking.StartDateTime) > 0) || 
                        (DateTime.Compare(oldBooking.StartDateTime, booking.EndDateTime) < 0 && DateTime.Compare(oldBooking.EndDateTime, booking.EndDateTime) > 0)))
                    {
                        timeIsVaild = false; break;
                    }
                }
                //checking if date is after today and endtime should be after starttime
                if (DateTime.Compare(DateTime.Now, booking.Date) >= 0 || DateTime.Compare(booking.StartDateTime, booking.EndDateTime) > 0)
                {
                    timeIsVaild = false;
                }
                //if starttime and endtime is valid, save the booking
                if (timeIsVaild)
                {
                    if (User.IsInRole("Patient"))
                    {
                        var userId = User.Identity.GetUserId();
                        var patient = db.Patients.Where(p => p.UserId == userId).ToList()[0];
                        booking.PatientId = patient.Id;
                    }
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName", booking.DentistId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", booking.LocationId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName", booking.PatientId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName", booking.DentistId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", booking.LocationId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName", booking.PatientId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,StartDateTime,EndDateTime,LocationId,DentistId,PatientId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Patient"))
                {
                    var userId = User.Identity.GetUserId();
                    var patient = db.Patients.Where(p => p.UserId == userId).ToList()[0];
                    booking.PatientId = patient.Id;
                }
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DentistId = new SelectList(db.Dentists, "Id", "DisplayName", booking.DentistId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", booking.LocationId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "DisplayName", booking.PatientId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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

