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
namespace SoftSchool.Controllers
{
    public class AccountController : Controller
    {
        private dbsoftschoolEntities1 db = new dbsoftschoolEntities1();
        //
        // GET: /Account/LogOn

        public ActionResult LogIn()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Logon");

            }
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogIn(LogOnModel model, string returnUrl)
        {
           // if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                       // return RedirectToAction("Index", "Index");
                        String[] rols = Roles.GetRolesForUser(model.UserName);
                        string str = rols.SingleOrDefault(s => s == "Administrateur");
                        if (str != null)
                        {
                            str = System.Web.HttpContext.Current.Request.Url.Authority;
                            return Json(new { result = "ok", user = model.UserName, url = "http://" + str + "/admin/Home/Index" });
                        }

                        string str1 = rols.SingleOrDefault(s => s == "Direction");
                        if (str1 != null)
                        {
                            str1 = System.Web.HttpContext.Current.Request.Url.Authority;
                            return Json(new { result = "ok", user = model.UserName, url = "http://" + str1 + "/Direction_regional/Direction_reg/Index" });
                        }

                        string str2 = rols.SingleOrDefault(s => s == "Utilisateur");
                        if (str2 != null)
                        {
                            str2 = System.Web.HttpContext.Current.Request.Url.Authority;
                            return Json(new { result = "ok", user = model.UserName, url = "http://" + str2 + "/Directeur/Direction/Index" });
                        }
                        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "الرجاء التثبل من إسم المستخدم و كلمة السر.");
                }
            }
            // If we got this far, something failed, redisplay form
            return PartialView("_Logon", model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index1", "Index");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            ViewBag.Dres = db.dres;
            ViewBag.Lycees = db.lycees;
            ViewBag.villes = db.villes;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Register");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetVilles(string id)
        {
            try
            {
                int idd = 0;
                int.TryParse(id, out idd);
                var data = db.villes.Where(l => l.DreID == idd).ToList();
                var colourData = data.Select(c => new SelectListItem()
                {
                    Text = c.Nom_Ar,
                    Value = c.VilleID.ToString(),
                });

                return Json(colourData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetLycees(string id)
        {
            try
            {
                int idd = 0;
                int.TryParse(id, out idd);
                var data = db.lycees.Where(l => l.VilleId == idd).ToList();
                var colourData = data.Select(c => new SelectListItem()
                {
                    Text = c.Nom,
                    Value = c.LyceeID.ToString(),
                });

                return Json(colourData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetUSERS(string id)
        {
            try
            {
                int idd = 0;
                int.TryParse(id, out idd);
                var data = db.utilisateur.Include(l => l.lycees).Where(l => l.lycees.LyceeID == idd).ToList();
                var colourData = data.Select(c => new SelectListItem()
                {
                    Text = c.Nom + " " + c.Prenom,
                    Value = c.aspnet_user.ToString(),
                });

                return Json(colourData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, message = ex.Message });
            }
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IDLycees != 0)
                {
                    // Attempt to register the user
                    MembershipCreateStatus createStatus;
                    MembershipUser user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, false, null, out createStatus);
                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        Roles.AddUserToRole(model.UserName, "Utilisateur");
                        my_aspnet_users aspuser = db.my_aspnet_users.Single(u => u.name == user.UserName);
                        utilisateur appuser = new utilisateur();
                        appuser.IDLycees = model.IDLycees;
                        appuser.Nom = model.Nom;
                        appuser.Prenom = model.Prenom;
                        appuser.aspnet_user = aspuser.id;
                        db.utilisateur.AddObject(appuser);
                        //db.ObjectStateManager.ChangeObjectState(appuser, EntityState.Modified);
                        db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (createStatus == MembershipCreateStatus.DuplicateEmail)
                        {
                            ModelState.AddModelError("", "عنوان البريد الإلكتروني موجود مسبقا");
                        }
                        else if (createStatus == MembershipCreateStatus.InvalidEmail)
                        {
                            ModelState.AddModelError("", "الرجاء التحقق من عنوان البريد الإلكتروني");
                        }
                        else if (createStatus == MembershipCreateStatus.InvalidUserName)
                        {
                            ModelState.AddModelError("", "الرجاء التحقق من اسم المستخدم الخاص بك");
                        }
                        else if (createStatus == MembershipCreateStatus.DuplicateUserName)
                        {
                            ModelState.AddModelError("", "اسم المستخدم موجود بالفعل");
                        }
                        else 
                        ModelState.AddModelError("", "");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "يرجى اختيار المؤسسة");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.model = model;
            ViewBag.Lycees = db.lycees;
            ViewBag.villes = db.villes;
            ViewBag.Dres = db.dres;
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
