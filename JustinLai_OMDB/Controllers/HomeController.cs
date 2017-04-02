using JustinLai_OMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustinLai_OMDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "OMDB Landing Page";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetMoviesByTitle(string title)
        {
            
            return Json("");
        }
        
    }
}