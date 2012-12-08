using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftSchool.Models;
namespace SoftSchool.Areas.Admin.Controllers
{
    public class CleController : Controller
    {
        dbsoftschoolEntities1 db = new dbsoftschoolEntities1();
        //
        // GET: /Admin/Cle/

        public ActionResult Index(Nullable<int> page, String l= "", String l1="")
        {
            RechercheCleLyce recherche = new RechercheCleLyce();
            if (page == null)
                page = 1;
            var lc = new List<activation>();
            if (l == "" && l1 == "")
                lc = db.activation.Include("logiciels").Include("utilisateur").OrderByDescending(m => m.date_demande).ToList();
            else
            {
                recherche.Lycees = l;
                recherche.Logiciel = l1;
                ViewBag.Lycees = recherche.Lycees;
                ViewBag.Logiciel = recherche.Logiciel;
                lc = find(recherche);
            }
            Tuple<IEnumerable<SoftSchool.Models.activation>, SoftSchool.Models.RechercheCleLyce> model = new Tuple<IEnumerable<activation>, RechercheCleLyce>(pagination(lc, page.Value), recherche);
            return View(model);
        }

        public ActionResult rechreche(RechercheCleLyce cherche)
        {
            List<activation> lc = find(cherche);
            ViewBag.Lycees = cherche.Lycees;
            ViewBag.Logiciel = cherche.Logiciel;
            Tuple<IEnumerable<SoftSchool.Models.activation>, SoftSchool.Models.RechercheCleLyce> model = new Tuple<IEnumerable<activation>, RechercheCleLyce>(pagination(lc, 1), cherche);
          //  var model = db.activation.Include("utilisateur").Include("logiciels").Where(a => a.utilisateur.lycees.Nom.StartsWith(recherche.Lycees+"") || a.logiciels.Nom.StartsWith(recherche.Logiciel+"")).OrderBy(m => m.date_demande).ToList();
            return View("Index", model);
        }
        //
        // GET: /Admin/Cle/
        public ActionResult Repondre(int id)
        {
            var model = db.activation.Single(a => a.Id == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Repondre(activation model)
        {
            if(model.Cle_Reponse!=null)
                if (model.Cle_Reponse != "")
                {
                    model.date_reponse = DateTime.Now;
                    db.activation.Attach(model);
                    db.ObjectStateManager.ChangeObjectState(model, System.Data.EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            model = db.activation.Single(m => m.Id == model.Id);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = db.activation.Single(a => a.Id == id);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = db.activation.Single(a => a.Id == id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            activation activation = db.activation.Single(l => l.Id == id);
            db.activation.DeleteObject(activation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private List<activation> find(RechercheCleLyce recherche)
        {
            dbsoftschoolEntities1 db1 = new dbsoftschoolEntities1();
            List<activation> lc = new List<activation>();
            var model = db.activation.Include("logiciels").Include("utilisateur").OrderByDescending(m => m.date_demande);
            foreach (activation a in model)
            {
                utilisateur usr = db1.utilisateur.Single(u=>u.IDuser==a.IDuser);
                logiciels lg =  a.logiciels;
                if (usr.lycees.Nom.Contains(recherche.Lycees+"") && lg.Nom.Contains(recherche.Logiciel+""))
                {
                    lc.Add(a);
                }
            }
            return lc;
        }

        private List<SoftSchool.Models.activation> pagination(List<SoftSchool.Models.activation> model, int page)
        {
            int div = 10;
            ViewBag.current = page;
            int count = model.Count();
            int nbp = count / div;
            if ((count % div) != 0)
                nbp++;
            ViewBag.NbrPage = nbp;
            List<SoftSchool.Models.activation> lyc = new List<activation>();
            if(count!=0)
            {
                if (page < nbp)
                {
                    lyc = model.GetRange(((page - 1) * div), div);
                }
                else
                {
                    div = count - ((page - 1) * div);
                    lyc = model.GetRange(((page - 1) * div), div);
                }
            }
            return lyc;
        }
       
        
        
    }
}
