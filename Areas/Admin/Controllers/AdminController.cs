﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SoftSchool.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/

        public RedirectResult Index()
        {
            return Redirect("Admin/Home/Index");
        }

    }
}
