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
using System.IO;
using Mvc.Mailer;
using SoftSchool.Mailers;

namespace SoftSchool.Areas.Admin.Controllers
{
    public class AdminMailController : Controller
    {
        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/")); } //Path should! always end with '/'
        }

        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();
        //
        // GET: /Admin/mail/

        public ActionResult Index()
        {
            return RedirectToAction("InBox");
        }

        #region Inbox
        public ActionResult InBox(int page=1)
        {
            var model = db.outbox.ToList();
            model = pagination(model, page);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = db.outbox.SingleOrDefault(o => o.id == id);
            return View(model);        
        }

        public ActionResult Repondre(int id)
        {
            var model = db.outbox.SingleOrDefault(o => o.id == id);
            ViewBag.contact = model.id;
            return View(new inbox());
        }

        [HttpPost]
        public ActionResult Repondre(inbox model)
        {
            if (model.subject!=null && model.objet!=null)
            {
                if(model.file!=null)
                {
                    HttpPostedFileBase SourceFile = Request.Files["file"];
                    var inputStream = SourceFile.InputStream;
                    var fullName = StorageRoot + "Joint\\" + User.Identity.Name + "_" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_" + Path.GetFileName(Request.Files["file"].FileName).Replace(" ", "_");
                    model.file = User.Identity.Name + "_" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_" + Path.GetFileName(Request.Files["file"].FileName).Replace(" ", "_");
                    using (var fsi = new FileStream(fullName, FileMode.Append, FileAccess.Write))
                    {
                        var buffer = new byte[1024];
                        var l = inputStream.Read(buffer, 0, 1024);
                        while (l > 0)
                        {
                            fsi.Write(buffer, 0, l);
                            l = inputStream.Read(buffer, 0, 1024);
                        }
                        fsi.Flush();
                        fsi.Close();
                    }
                }
                model.UserId = getuser().aspnet_user;
                outbox ot = db.outbox.SingleOrDefault(o => o.id == model.Contact);
                model.Contact = ot.UserId;
                model.date_envoie = DateTime.Now;
                db.inbox.AddObject(model);

                ot.contact = model.UserId;
                db.ObjectStateManager.ChangeObjectState(ot, EntityState.Modified);

                db.SaveChanges();
                
                ViewBag.contact = model.UserId;
                return RedirectToAction("InBox");
            }
            else
            {
                return View(model);
            }
        }

        private List<SoftSchool.Models.outbox> pagination(List<SoftSchool.Models.outbox> model, int page)
        {
            int div = 10;
            ViewBag.current = page;
            int count = model.Count();
            int nbp = count / div;
            if ((count % div) != 0)
                nbp++;
            ViewBag.NbrPage = nbp;
            List<SoftSchool.Models.outbox> lyc = new List<outbox>();
            if (count != 0)
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


        #endregion
        
        #region Outox

        public ActionResult OutBox(int page = 1)
        {
            var model = db.inbox.ToList();
            model = pagination(model, page);
            return View(model);
        }

        public ActionResult DetailsOutBox(int id)
        {
            var model = db.inbox.SingleOrDefault(o => o.ID == id);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = db.inbox.SingleOrDefault(i => i.ID == id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            inbox activation = db.inbox.Single(l => l.ID == id);
            db.inbox.DeleteObject(activation);
            db.SaveChanges();
            return RedirectToAction("OutBox");
        }

        public ActionResult Nouveau()
        {
            inbox model = new inbox();
            ViewBag.Dres = db.dres.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nouveau(inbox model)
        {
            model.UserId = getuser().aspnet_user;

            if (model.file != null)
            {
                HttpPostedFileBase SourceFile = Request.Files["file"];
                var inputStream = SourceFile.InputStream;
                var fullName = StorageRoot + "Joint\\" + User.Identity.Name + "_" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_" + Path.GetFileName(Request.Files["file"].FileName).Replace(" ", "_");
                model.file = User.Identity.Name + "_" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_" + Path.GetFileName(Request.Files["file"].FileName).Replace(" ", "_");
                using (var fsi = new FileStream(fullName, FileMode.Append, FileAccess.Write))
                {
                    var buffer = new byte[1024];
                    var l = inputStream.Read(buffer, 0, 1024);
                    while (l > 0)
                    {
                        fsi.Write(buffer, 0, l);
                        l = inputStream.Read(buffer, 0, 1024);
                    }
                    fsi.Flush();
                    fsi.Close();
                }
            }
            model.date_envoie = DateTime.Now;
            db.inbox.AddObject(model);
            db.SaveChanges();
            ViewBag.Dres = db.dres.ToList();
            my_aspnet_membership mem= db.my_aspnet_membership.SingleOrDefault(x => x.userId == model.Contact);
            new WelcomeMailer().Welcome(mem, model.subject, model.objet).Send();
            return RedirectToAction("OutBox");
        }

        private List<SoftSchool.Models.inbox> pagination(List<SoftSchool.Models.inbox> model, int page)
        {
            int div = 10;
            ViewBag.current = page;
            int count = model.Count();
            int nbp = count / div;
            if ((count % div) != 0)
                nbp++;
            ViewBag.NbrPage = nbp;
            List<SoftSchool.Models.inbox> lyc = new List<inbox>();
            if (count != 0)
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


       

        #endregion

       

        private utilisateur getuser()
        {
            my_aspnet_users us = db.my_aspnet_users.SingleOrDefault(u => u.name == User.Identity.Name);
            if (us != null)
            {
                return db.utilisateur.SingleOrDefault(u => u.aspnet_user == us.id);
            }
            else
                return null;
        }
    }
}
