using FIT5032_Assignment_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment_v1.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private FIT5032_Models db = new FIT5032_Models();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Mark = db.Dentists.Find(1).AggregatedRating.ToString();
            ViewBag.Darby = db.Dentists.Find(2).AggregatedRating.ToString();
            ViewBag.Jacky = db.Dentists.Find(3).AggregatedRating.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LiveChat()
        {
            return View();
        }
    }
}