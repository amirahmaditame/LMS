using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult ActivationEmail()
        {
            return PartialView();
        }
        public ActionResult RecoveryPassword()
        {
            return PartialView();
        }
    }
    
}