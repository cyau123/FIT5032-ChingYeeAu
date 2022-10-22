using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using FIT5032_Assignment_v1.Models;

namespace FIT5032_Assignment_v1.Controllers
{
    [Authorize(Roles = "Admin, Receptionist")]
    public class PatientsController : Controller
    {
        private FIT5032_Models db = new FIT5032_Models();

        // GET: Patients
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Gender,PhoneNumber,Email")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("insert into Patients(FirstName, LastName, Gender, PhoneNumber, Email) values (@FirstName, @LastName, @Gender, @PhoneNumber, @Email)", 
                    new SqlParameter("@FirstName", patient.FirstName), new SqlParameter("@LastName", patient.LastName), new SqlParameter("@Gender", patient.Gender), 
                    new SqlParameter("@PhoneNumber", patient.PhoneNumber), new SqlParameter("@Email", patient.Email));
                //db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Gender,PhoneNumber,Email")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("update Patients set FirstName=@FirstName, LastName=@LastName, Gender=@Gender, PhoneNumber=@PhoneNumber, Email=@Email where Id=@Id",
                    new SqlParameter("@Id", patient.Id),
                    new SqlParameter("@FirstName", patient.FirstName), new SqlParameter("@LastName", patient.LastName), new SqlParameter("@Gender", patient.Gender),
                    new SqlParameter("@PhoneNumber", patient.PhoneNumber), new SqlParameter("@Email", patient.Email));
                //db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
