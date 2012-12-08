using System.Web.Mvc;

namespace SoftSchool.Areas.Direction_regional
{
    public class Direction_regionalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Direction_regional";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Direction_regional_default",
                "Direction_regional/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
