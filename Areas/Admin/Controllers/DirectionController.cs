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
    public class DirectionController : Controller
    {
        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();

        //
        // GET: /Admin/Direction/

        public ViewResult Index()
        {
            var dres = db.dres.Include("gouvernorats");
            return View(dres.ToList());
        }

        //
        // GET: /Admin/Direction/Details/5

        public ViewResult Details(int id)
        {
            dres dres = db.dres.Single(d => d.DreId == id);
            return View(dres);
        }

        //
        // GET: /Admin/Direction/Create

        public ActionResult Create()
        {
            ViewBag.GovernoratID = new SelectList(db.gouvernorats, "GouvernoratID", "Nom_Ar");
            return View();
        } 

        //
        // POST: /Admin/Direction/Create

        [HttpPost]
        public ActionResult Create(dres dres)
        {
            if (ModelState.IsValid)
            {
                db.dres.AddObject(dres);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.GovernoratID = new SelectList(db.gouvernorats, "GouvernoratID", "Nom_Ar", dres.GovernoratID);
            return View(dres);
        }
        
        //
        // GET: /Admin/Direction/Edit/5
 
        public ActionResult Edit(int id)
        {
            dres dres = db.dres.Single(d => d.DreId == id);
            ViewBag.GovernoratID = new SelectList(db.gouvernorats, "GouvernoratID", "Nom_Ar", dres.GovernoratID);
            return View(dres);
        }

        //
        // POST: /Admin/Direction/Edit/5

        [HttpPost]
        public ActionResult Edit(dres dres)
        {
            if (ModelState.IsValid)
            {
                db.dres.Attach(dres);
                db.ObjectStateManager.ChangeObjectState(dres, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GovernoratID = new SelectList(db.gouvernorats, "GouvernoratID", "Nom_Ar", dres.GovernoratID);
            return View(dres);
        }

        //
        // GET: /Admin/Direction/Delete/5
 
        public ActionResult Delete(int id)
        {
            dres dres = db.dres.Single(d => d.DreId == id);
            return View(dres);
        }

        //
        // POST: /Admin/Direction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            dres dres = db.dres.Single(d => d.DreId == id);
            db.dres.DeleteObject(dres);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Lyceés
        public ActionResult Lycees(int id)
        {
            dres dres = db.dres.First(d => d.DreId == id);
            ViewBag.Dres = dres;
            var model = db.lycees.Include(v => v.villes).Where(l => l.villes.DreID == id);
            return View(model.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}