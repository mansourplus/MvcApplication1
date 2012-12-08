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

namespace SoftSchool.Areas.Directeur.Controllers
{
    public class CleActivationController : Controller
    {
        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();

        //
        // GET: /Directeur/CleActivation/

        public ActionResult Index()
        {
            var model = new List<activation>();
            my_aspnet_users us= db.my_aspnet_users.SingleOrDefault(u => u.name == User.Identity.Name);
            if (us != null)
            {
                utilisateur usr = db.utilisateur.SingleOrDefault(u => u.aspnet_user == us.id);
                if (us != null)
                {
                    lycees lc = db.lycees.SingleOrDefault(l => l.LyceeID == usr.IDLycees);
                    if(lc!=null)
                    {
                        try
                        {
                            model = db.activation.Include("logiciels").Where(a => a.IDuser == usr.IDuser).ToList();
                        }
                        catch { }
                    }
                }
            }
            return View(model);
        }

        //
        // GET: /Directeur/DemandeCle/
        public ActionResult DemandeCle()
        {
            activation model = new activation();
            Tuple<activation, List<logiciels>> mod = new Tuple<activation, List<logiciels>>(model, db.logiciels.ToList());
            return View(mod);
        }

        
        [HttpPost]
        public ActionResult DemandeCle(activation model)
        {
            if (ModelState.IsValid && model.LogicelId != -1 && model.Cle_Demander != null && model.Cle_Demander.Trim() != String.Empty)
            {
                db.activation.AddObject(model);
                var model1 = new List<activation>();
                lycees lc = get_lycee(User.Identity.Name);
                my_aspnet_users us = db.my_aspnet_users.SingleOrDefault(u => u.name == User.Identity.Name);
                utilisateur usr = db.utilisateur.SingleOrDefault(u => u.aspnet_user == us.id);
                model.IDuser = usr.IDuser;
                model.date_demande = DateTime.Now;
                db.SaveChanges();
                if (lc != null)
                {
                    try
                    {
                        model1 = db.activation.Include("logiciels").Where(a => a.IDuser == usr.IDuser).ToList();
                    }
                    catch { }
                }
                
                return View("Index",model1);
            }
            else
            {
                if (model.LogicelId == -1)
                {
                    ModelState.AddModelError("Item1.LogicelId", "الرجاء تحديد المنظومة");
                }
                if (model.Cle_Demander == String.Empty || model.Cle_Demander == null)
                {
                    ModelState.AddModelError("Item1.Cle_Demander", "الرجاء إدخال رمز التشغيل الخاص بالمؤسسة");
                }
            }
            Tuple<activation, List<logiciels>> mod = new Tuple<activation, List<logiciels>>(model, db.logiciels.ToList());
            return View(mod);
        }

        private lycees get_lycee(string username)
        {
            lycees lc=null;
            my_aspnet_users us = db.my_aspnet_users.SingleOrDefault(u => u.name == User.Identity.Name);
            if (us != null)
            {
                utilisateur usr = db.utilisateur.SingleOrDefault(u => u.aspnet_user == us.id);
                if (us != null)
                {
                    lc = db.lycees.SingleOrDefault(l => l.LyceeID == usr.IDLycees);
                }
            }
            return lc;
        }

    }
}
