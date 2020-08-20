using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;

namespace LMS.Areas.Admin.Controllers
{
    public class CourseSearchController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();

        private INotificationRepository _notificationRepository;

        public CourseSearchController()
        {
            _notificationRepository = new NotificationRepository(db);
        }
        // GET: Admin/CourseSearch
        public ActionResult Notifications()
        {
            //var notification = _notificationRepository.
            return View();
        }
    }
}