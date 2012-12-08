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

namespace SoftSchool.Areas.Directeur.Controllers
{
    public class MailController : Controller
    {

        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/")); } //Path should! always end with '/'
        }

        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();
        //
        // GET: /Directeur/Mail/
        public ActionResult Index(int page=1)
        {
            var model = new List<inbox>();
            my_aspnet_users us= db.my_aspnet_users.SingleOrDefault(u => u.name == User.Identity.Name);
            if (us != null)
            {
                utilisateur usr = db.utilisateur.SingleOrDefault(u => u.aspnet_user == us.id);
               model = db.inbox.Where(m => m.Contact == us.id).ToList();
            }
            return View(paginationI(model,page));
        }
        public ActionResult DetailsInbox(int id)
        {
            inbox outb = db.inbox.SingleOrDefault(o => o.ID == id);
            return View(outb);
        }


        //
        //GET:/Directeur/Mail/outbox
        public ActionResult outbox(int page=1)
        {
            var model = new List<outbox>();
            my_aspnet_users us = db.my_aspnet_users.SingleOrDefault(u => u.name == User.Identity.Name);
            if (us != null)
            {
                utilisateur usr = db.utilisateur.SingleOrDefault(u => u.aspnet_user == us.id);
                model = db.outbox.Where(m => m.UserId == us.id).ToList();
            }
            return View(paginationO(model,page));
        }

        public ActionResult Detailsoutbox(int id)
        {
            outbox outb = db.outbox.SingleOrDefault(o => o.id == id);
            return View(outb);
        }

        //
        // GET: /Directeur/Mail/Send
        public ActionResult Send()
        {
            outbox outb = new outbox();
            utilisateur usr = getuser();
            if (usr != null)
            {
                outb.UserId = usr.aspnet_user;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Send(outbox outb)
        {
            utilisateur usr = getuser();
            if (usr != null && outb.subject != null )
            {
                if (outb.file != null)
                {
                    HttpPostedFileBase SourceFile = Request.Files["file"];
                    var inputStream = SourceFile.InputStream;
                    var fullName = StorageRoot + "Joint\\" + User.Identity.Name + "_" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_" + Path.GetFileName(Request.Files["file"].FileName).Replace(" ", "_");
                    outb.file = User.Identity.Name + "_" + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + "_" + Path.GetFileName(Request.Files["file"].FileName).Replace(" ", "_");
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
                outb.UserId = usr.aspnet_user;
                outb.date_envoie = DateTime.Now;
                db.outbox.AddObject(outb);
                db.SaveChanges();
                return RedirectToAction("outbox");
            }
            else
            {
                if (outb.subject == null)
                    ModelState.AddModelError("subject", " ");
                
            }
            return View(outb);
        }

        private List<SoftSchool.Models.outbox> paginationO(List<SoftSchool.Models.outbox> model, int page)
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

        private List<SoftSchool.Models.inbox> paginationI(List<SoftSchool.Models.inbox> model, int page)
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
