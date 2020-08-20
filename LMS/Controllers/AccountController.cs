using DataLayer;
using DataLayer.Repositories;
using DataLayer.Services;
using DateLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace LMS.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        LearningDBEntities db = new LearningDBEntities();
        public AccountController()
        {
            _userRepository = new UserRepository(db);
        }

        #region Register
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.UserExist(register.Email))
                {
                    string HashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5");
                    Users users = new Users()
                    {
                        UserName = register.UserName,
                        Email = register.Email.Trim().ToLower(),
                        Password = HashPass,
                        IsActive = false,
                        ActiveCode = Guid.NewGuid().ToString(),
                        RegisterDate = DateTime.Now,
                        RoleID = 1,
                        
                    };
                    _userRepository.InsertUser(users);
                    _userRepository.save();


                    //string Body = PartialToStringClass.RenderPartialView("Email", "ActivationEmail", users);
                    //SendEmail.Send(users.Email, "Regisertion", Body);

                    return View("_SuccessRegister", users);

                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده وجود دارد ");
                }
            }
            return View(register);
        }

        #endregion

        #region Login
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                string HashPass = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");

                var user = _userRepository.GetUserForLogin(login.Email, HashPass);
                if (user != null)
                {
                    if (user.IsActive == true)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ViewBag.Error = true;
                    }
                }
                else
                {
                    ModelState.AddModelError("Email","ایمیل با پسورد اشتباه وارد شده است");
                    return View(login);
                }
            }
            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        #endregion


        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserForForgotPassword(forgot.Email);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        //string bodyEmail = PartialToStringClass.RenderPartialView("Email", "RecoveryPassword", user);
                        //SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
                        return View("_SuccesForgotPassword", user);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نیست");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربری یافت نشد");
                }
            }
            return View();
        }

        public ActionResult RecoveryPassword(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoveryPassword(string id, RecoveyPasswordViewModel recovery)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserForActiveCode(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recovery.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString();
                _userRepository.save();
                return Redirect("/Login?recovery=true");
            }
            return View();
        }


        public ActionResult ActiveUser(string id)
        {
            var user = _userRepository.GetUserForActiveCode(id);
            if (user != null)
            {
                user.IsActive = true;
                user.ActiveCode = Guid.NewGuid().ToString();
                _userRepository.save();

                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}