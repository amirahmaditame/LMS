using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {

        private ISearchRepository _searchRepository;
        private IHomeRepository _homeRepository;
        private IPageRepository _pageRepository;
        LearningDBEntities db = new LearningDBEntities();
        public HomeController()
        {
            _searchRepository = new SearchRepository(db);
            _homeRepository = new HomeRepository(db);
            _pageRepository = new PagesRepository(db);
        }

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
            return PartialView(_pageRepository.LastNews());
        }
        public ActionResult popularCategory()
        {
            return PartialView();
        }
        public ActionResult popularCourse()
        {
            return PartialView();
        }
        public ActionResult FeaturedCourses()
        {
            return PartialView(_homeRepository.GetAllCourses());
        }
        public ActionResult Search()
        {
        
            ViewBag.courseCount = _searchRepository.courseCount();
            ViewBag.teacherCount = _searchRepository.teacherCount();
            ViewBag.userCount = _searchRepository.userCount();
            return PartialView();
        }
    }
}