using System;
using System.Collections.Generic;
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

namespace LMS.Areas.UserPanel.Controllers
{
    public class DashboardController : Controller
    {
        private LearningDBEntities db = new LearningDBEntities();

        private IUserRepository _userRepository;

        public DashboardController()
        {
            _userRepository = new UserRepository(db);
        }
        // GET: UserPanel/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountSetting()
        {
            var user = _userRepository.GetUserByUserNameForProfile(User.Identity.Name);
            ViewBag.SuccessProfile = TempData["SuccessProfile"];
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AccountSetting")]
        public ActionResult AccountSetting(User_PF user, HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                if (ImgUp != null && ImgUp.IsImage())
                {
                    if (user.ImageName != "no-avatar.png")
                    {
                        System.IO.File.Delete(Server.MapPath("/Images/Student/" + user.ImageName));
                    }
                    user.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/Images/Student/" + user.ImageName));

                }

                _userRepository.UpdateUserProfile(user);
                _userRepository.save();
                TempData["SuccessProfile"] = true;
                return RedirectToAction("AccountSetting");

            }
            return View(user);
        }

        public ActionResult ChangePassword()
        {
            ViewBag.SuccessPass = TempData["SuccessPass"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel changePassword)
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
                    return RedirectToAction("ChangePassword");
                }
                ModelState.AddModelError("OldPassword","رمز فعلی اشتباه وارد شده است");
            }
            return View(changePassword);

        }
    }
}