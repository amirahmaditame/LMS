using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class SearchController : Controller
    {

        private ISearchRepository _searchRepository;
        LearningDBEntities db = new LearningDBEntities();
        public SearchController()
        {
            _searchRepository = new SearchRepository(db);
        }

        [Route("Search")]
        public ActionResult Index(string q)
        {
            return View(_searchRepository.searchCourses(q));
        }
    }
}