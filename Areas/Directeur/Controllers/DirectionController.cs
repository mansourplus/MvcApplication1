using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftSchool.Models;
using System.IO;
namespace SoftSchool.Areas.Directeur.Controllers
{
    [Authorize(Roles = "Utilisateur")]
    public class DirectionController : Controller
    {
        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/")); } //Path should! always end with '/'
        }

        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();

        //
        // GET: /Directeur/Direction/

        public ActionResult Index()
        {
            return View(db.logiciels.ToList());
        }

        public FileResult download(int id)
        {
            logiciels l = db.logiciels.Single(lg => lg.LogicielID == id);

            string fullName = StorageRoot+ l.Lien;
            try
            {
                byte[] fileBytes = GetFile(fullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Setup_"+l.Nom+".exe");
            }
            catch 
            {
                return null;
            }
 
        }

        byte[] GetFile(string s)
        {

            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

    }
}
