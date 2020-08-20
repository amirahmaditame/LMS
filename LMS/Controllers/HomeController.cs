using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        public ActionResult Subscribe()
        {
            return PartialView();
        }
        public ActionResult RecentArticles()
        {
            return PartialView();
        }
        public ActionResult popularCategory()
        {
            return PartialView();
        }
        public ActionResult FeaturedCourses()
        {
            return PartialView();
        }
        public ActionResult Search()
        {
            return PartialView();
        }
    }
}