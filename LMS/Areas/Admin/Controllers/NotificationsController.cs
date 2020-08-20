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
    public class NotificationsController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();

        private INotificationRepository _notificationRepository;

        public NotificationsController()
        {
            _notificationRepository = new NotificationRepository(db);
        }
        // GET: Admin/CourseSearch
        public ActionResult Notifications()
        {
            var notifications = _notificationRepository.GetAllnotificationsByUserNotRead(User.Identity.Name);
            return PartialView(notifications.ToList());
        }

        public ActionResult NotificationsList(string q = "", int take = 1, int pageid = 1)
        {
            var NotificationsList = _notificationRepository.GetAllnotificationsByUser(User.Identity.Name,q);
            int skip = (pageid - 1) * take;
            ViewBag.PageCount = NotificationsList.Count() / take;
            ViewBag.pageid = pageid;
            ViewBag.Search = q;

            return View(NotificationsList.Distinct().Skip(skip).Take(take).ToList());
        }

        public void DeleteNotification(int id)
        {
            _notificationRepository.DeleteNotifications(id);
            _notificationRepository.save();
        }
    }
}