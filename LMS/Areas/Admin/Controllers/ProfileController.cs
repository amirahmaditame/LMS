using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using DateLayer;
using KooyWebApp_MVC.Classes;

namespace LMS.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();
        private IUserRepository _userRepository;
        public ProfileController()
        {
            _userRepository = new UserRepository(db);
        }
        // GET: Admin/Profile
        public ActionResult Index()
        {
            var Teacher = _userRepository.GetTeacherByUserNameForProfile(User.Identity.Name);

            ViewBag.WrongPass = TempData["WrongPass"];
            ViewBag.SuccessPass = TempData["SuccessPass"];
            ViewBag.SuccessProfile = TempData["SuccessProfile"];
            return View(Teacher);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Teachers_PF teacher, HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                if (ImgUp != null && ImgUp.IsImage())
                {
                    if (teacher.ImageName != "no-avatar.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/Teachers/" + teacher.ImageName));
                    }
                    teacher.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/Teachers/" + teacher.ImageName));

                }


                _userRepository.UpdateTeacherProfile(teacher);
                _userRepository.save();
                TempData["SuccessProfile"] = true;
                return RedirectToAction("Index");
            }
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChnagePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {

                string OldHashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(changePassword.OldPassword, "MD5");

                var user = _userRepository.GetUserForForChangePassword(OldHashPass, User.Identity.Name);

                var NewHashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(changePassword.Password, "MD5");

                if (user != null)
                {
                    user.Password = NewHashPass;
                    TempData["SuccessPass"] = true;
                    _userRepository.save();
                    return RedirectToAction("Index");
                }
                TempData["WrongPass"] = true;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}