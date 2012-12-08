using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftSchool.Models;
namespace SoftSchool.Areas.Direction_regional.Controllers
{
    [Authorize(Roles = "Direction")]
    public class Direction_regController : Controller
    {
        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();

        //
        // GET: /Direction_regional/Direction_reg/

        public ActionResult Index()
        {
            return View(db.logiciels.ToList());
        }

    }
}
