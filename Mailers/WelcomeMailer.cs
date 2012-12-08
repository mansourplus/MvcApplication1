using Mvc.Mailer;
using System;
using System.Collections.Generic;
namespace SoftSchool.Mailers
{ 
    public class WelcomeMailer : MailerBase, IWelcomeMailer 	
	{
		public WelcomeMailer()
		{
			MasterName="_LayoutMail";
		}
		
		public virtual MvcMailMessage Welcome(List<SoftSchool.Models.my_aspnet_membership> Listuser)
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Welcome";
				x.ViewName = "Welcome";
                foreach (SoftSchool.Models.my_aspnet_membership usr in Listuser)
                {
                    x.To.Add(usr.Email);
                }
			});
		}

        public virtual MvcMailMessage Welcome(SoftSchool.Models.my_aspnet_membership usr,String Subject, String Objet)
        {
            //ViewBag.Data = someObject;
            ViewBag.Subject = Subject;
            ViewBag.Object = Objet;
            return Populate(x =>
            {
                x.Subject = "Welcome";
                x.ViewName = "Welcome";
                x.To.Add(usr.Email);
            });
        }
 	}
}