using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftSchool.Models
{
    public class RechercheCleLyce
    {
        public string Lycees { get; set; }

        public string Logiciel { get; set; }
    }
}