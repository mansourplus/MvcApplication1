using Mvc.Mailer;
using System;
namespace SoftSchool.Mailers
{ 
    public interface IWelcomeMailer
    {
        MvcMailMessage Welcome(SoftSchool.Models.my_aspnet_membership usr, String Subject, String Objet);
	}
}