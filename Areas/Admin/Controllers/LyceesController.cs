using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftSchool.Models;

namespace SoftSchool.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class LyceesController : Controller
    {
        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();

        //
        // GET: /Admin/Lycees/

        public ViewResult Index()
        {
            var lycees = db.lycees.Include("Villes");
            return View(lycees.ToList());
        }

        //
        // GET: /Admin/Lycees/Details/5

        public ViewResult Details(int id)
        {
            lycees lycees = db.lycees.Single(l => l.LyceeID == id);
            ViewBag.DREID = db.villes.Single(v => v.VilleID == lycees.VilleId).DreID;
            return View(lycees);
        }

        //
        // GET: /Admin/Lycees/Create

        public ActionResult Create(int id)
        {
            ViewBag.VilleId = new SelectList(db.villes.Where(v=>v.DreID==id), "VilleID", "Nom_Ar");
            ViewBag.DREID = db.villes.First(v => v.DreID == id).DreID;
            return View();
        } 

        //
        // POST: /Admin/Lycees/Create

        [HttpPost]
        public ActionResult Create(lycees lycees)
        {
            if (ModelState.IsValid)
            {
                db.lycees.AddObject(lycees);
                try
                {
                    db.SaveChanges();
                }
                catch { }
                return RedirectToAction("Lycees/"+lycees.villes.DreID, "Direction");
            }

            ViewBag.VilleId = new SelectList(db.villes, "VilleID", "Nom_Ar", lycees.VilleId);
            return View(lycees);
        }
        
        //
        // GET: /Admin/Lycees/Edit/5
 
        public ActionResult Edit(int id)
        {
            lycees lycees = db.lycees.Single(l => l.LyceeID == id);
            ViewBag.DREID = db.villes.Single(v => v.VilleID == lycees.VilleId).DreID;
            ViewBag.VilleId = new SelectList(db.villes.Where(v=>v.DreID== lycees.villes.DreID), "VilleID", "Nom_Ar", lycees.VilleId);
            return View(lycees);
        }

        //
        // POST: /Admin/Lycees/Edit/5

        [HttpPost]
        public ActionResult Edit(lycees lycees)
        {
            if (ModelState.IsValid)
            {
                db.lycees.Attach(lycees);
                db.ObjectStateManager.ChangeObjectState(lycees, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Lycees/" + lycees.villes.DreID, "Direction");
            }
            ViewBag.VilleId = new SelectList(db.villes, "VilleID", "Nom_Ar", lycees.VilleId);
            return View(lycees);
        }

        //
        // GET: /Admin/Lycees/Delete/5
 
        public ActionResult Delete(int id)
        {
            lycees lycees = db.lycees.Single(l => l.LyceeID == id);
            ViewBag.DREID = db.villes.Single(v => v.VilleID == lycees.VilleId).DreID;
            return View(lycees);
        }

        //
        // POST: /Admin/Lycees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            lycees lycees = db.lycees.Include(v=>v.villes).Single(l => l.LyceeID == id);
            villes ville = db.villes.Single(v => v.VilleID == lycees.VilleId);
            db.lycees.DeleteObject(lycees);
            db.SaveChanges();
            return RedirectToAction("Lycees/" + ville.DreID, "Direction");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}