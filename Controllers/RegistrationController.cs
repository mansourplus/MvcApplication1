using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftSchool.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/

        public ActionResult Connect()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verif()
        {
            return View();
        }

    }
}
