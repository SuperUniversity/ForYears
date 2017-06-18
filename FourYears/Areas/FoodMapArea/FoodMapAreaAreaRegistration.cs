using System.Web.Mvc;

namespace FourYears.Areas.FoodMapArea
{
    public class FoodMapAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FoodMapArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FoodMapArea_default",
                "FoodMapArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}