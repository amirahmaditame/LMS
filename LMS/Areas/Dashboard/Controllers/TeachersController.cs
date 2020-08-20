using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;

namespace LMS.Areas.Dashboard.Controllers
{
    public class TeachersController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();
        private ITeacherRepository _teacherRepository;
        private INotificationRepository _notificationRepository;

        public TeachersController()
        {
            _teacherRepository = new TeacherRepository(db);
            _notificationRepository = new NotificationRepository(db);
        }

        // GET: Admin/Categories
        public ActionResult Index()
        {
            var teachers = _teacherRepository.GetAllTeacher();
            return View(teachers.ToList());
        }

        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users teachers)
        {
            if (ModelState.IsValid)
            {
                var Teacheradd = new Users()
                {
                    Email = teachers.Email,
                    ActiveCode = Guid.NewGuid().ToString(),
                    IsActive = true,
                    Password = teachers.Password,
                    RegisterDate = DateTime.Now,
                    RoleID = 2,
                    UserName = teachers.UserName
                };


                _teacherRepository.InsertTeacher(Teacheradd);
                _teacherRepository.save();

                return RedirectToAction("Index");
            }

            return PartialView(teachers);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users teacher = _teacherRepository.GetTeacherById(id.Value);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            return PartialView(teacher);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] Users teacher)
        {
            if (ModelState.IsValid)
            {
                _teacherRepository.UpdateTeacher(teacher);
                _teacherRepository.save();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Admin/Categories/Delete/5
        public void Delete(int id)
        {
            var teacher = _teacherRepository.GetTeacherById(id);

            _teacherRepository.DeleteTeacher(teacher);
            _teacherRepository.save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                _teacherRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SendNotification(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SendNotification(Notifications notif , int id)
        {
            if (ModelState.IsValid)
            {
                var notifcation = new Notifications()
                {
                    NotificationDate = DateTime.Now,
                    NotificationText = notif.NotificationText,
                    NotificationTitle = notif.NotificationTitle,
                    UserID = id
                };

                _notificationRepository.InsertNotifications(notifcation);
                _notificationRepository.save();

                return RedirectToAction("Index");
            }
            return PartialView(notif);
        }
    }
}
