using System.Web.Mvc;

namespace SoftSchool.Areas.Directeur
{
    public class DirecteurAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Directeur";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Directeur_default",
                "Directeur/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
