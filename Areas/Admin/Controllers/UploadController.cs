using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace SoftSchool.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class UploadController : Controller
    {
        //
        // GET: /Admin/Upload/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult charger()
        {
            return View();
        }

        [HttpPost]
        public ActionResult charger(Models.logiciels log)
        {

            if (Request.Files.Count == 0)
                return new ContentResult() { ContentType = "text/plain", Content = "File upload failed." };
            else
            {
                HttpPostedFileBase SourceFile = Request.Files[0];
                ContentResult result = new ContentResult();
                result.ContentType = "text/plain";

                StreamReader reader = new StreamReader(SourceFile.InputStream);
                string content = reader.ReadToEnd();
                result.Content = content;

                return result;
            }
            return View();
        }

    }
}
