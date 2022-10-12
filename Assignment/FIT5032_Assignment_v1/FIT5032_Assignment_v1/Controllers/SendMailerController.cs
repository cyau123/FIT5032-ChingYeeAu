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
        //  
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

    }
}