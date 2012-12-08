using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftSchool.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit être d'au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "La confirmation du mot de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "الرجاء إدخال إسم المستخدم")]
        [Display(Name = "إسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال كلمة السر")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [Display(Name = "تذكرني؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "الرجاء إدخال الإسم")]
        [Display(Name = "الإسم")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال اللقب")]
        [Display(Name = "اللقب")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال إسم المستخدم")]
        [Display(Name = "إسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال البريد الإلكتروني")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الرجاء تحديد المؤسسة التربوية")]
        [Display(Name = "المؤسسة التربوية")]
        public int IDLycees { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال كلمة السر")]
        [StringLength(100, ErrorMessage = "يجب أن تكون {0} في {2} الأقل حرفا.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Mot de passe")]
        [Compare("Password", ErrorMessage = "تأكيد كلمة السر  غير متطابقة.")]
        public string ConfirmPassword { get; set; }
    }
}
