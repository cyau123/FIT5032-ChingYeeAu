using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment_v1.Utils
{
    public class BulkEmailSender
    {
        private const String API_KEY = "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";

        public void Send(List<String> toEmail, String subject, String contents, HttpPostedFileBase path)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("cyau234@gmail.com", "HQ Dental Group");
            var to = new List<EmailAddress>();
            foreach (String email in toEmail)
            {
                to.Add(new EmailAddress(email));
            }
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
            if (path != null)
            {
                string fileName = Path.GetFileName(path.FileName);
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/"), fileName);
                path.SaveAs(filePath);
                var bytes = File.ReadAllBytes(filePath);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(fileName, file, "image/jpeg");
            }
            var response = client.SendEmailAsync(msg);
        }
    }
}