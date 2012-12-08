using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SoftSchool.Models;
using System.Data;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using MySql.Web;
using MySql.Web.Security;
namespace SoftSchool.Controllers
{
    public class IndexController : Controller
    {
        dbsoftschoolEntities1 db = new dbsoftschoolEntities1();
        public RedirectToRouteResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index1", "Index");
            }
            String[] rols = Roles.GetRolesForUser(User.Identity.Name);
            string str = rols.SingleOrDefault(s => s == "Administrateur");
            if (str != null)
            {
                return RedirectToAction("index", "Home", new { area = "admin" });
            }

            string str1 = rols.SingleOrDefault(s => s == "Direction");
            if (str1 != null)
            {
                return RedirectToAction("Index", "Direction_reg", new { area = "Direction_regional" });
            }

            string str2 = rols.SingleOrDefault(s => s == "Utilisateur");
            if (str2 != null)
            {
                return RedirectToAction("Index", "Direction", new { area = "Directeur" });
            }
            return null;
        }

        public ActionResult Index1()
        {
             ViewBag.Message = "";
            var model = db.logiciels;
            Tuple<IEnumerable<SoftSchool.Models.logiciels>, SoftSchool.Models.LogOnModel> mod = new Tuple<IEnumerable<logiciels>, LogOnModel>(model.ToList(), null);
            return View("Index", model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult DetailLog(int id)
        {
            var log = db.logiciels.Single(l => l.LogicielID == id);
            return View(log);
        }
    }
}
