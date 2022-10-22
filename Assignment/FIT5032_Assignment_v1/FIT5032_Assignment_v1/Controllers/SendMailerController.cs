using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_v1.Models;
using FIT5032_Assignment_v1.Utils;

namespace FIT5032_Assignment_v1.Controllers
{
    public class SendMailerController : Controller
    {
        private FIT5032_Models db = new FIT5032_Models();
        // GET: /SendMailer/  
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model, HttpPostedFileBase path)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    HttpPostedFileBase newFilePath = model.Path;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents, path);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult Send_Bulk_Email()
        {
            return View(new SendBulkEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Bulk_Email(SendBulkEmailViewModel model, HttpPostedFileBase path)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var patientList = db.Patients.SqlQuery("Select * from Patients").ToList<Patient>();
                    List<String> toEmail = new List<String>();
                    foreach (var patient in patientList)
                    {
                        toEmail.Add(patient.Email);
                    }
                    String subject = model.Subject;
                    String contents = model.Contents;
                    HttpPostedFileBase newFilePath = model.Path;

                    BulkEmailSender es = new BulkEmailSender();
                    es.Send(toEmail, subject, contents, path);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendBulkEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
}