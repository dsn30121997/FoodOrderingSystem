using System.Web.Mvc;
using System.Web.Routing;

namespace FoodOrderingSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }

            );



            routes.MapRoute(
               name: "CartDetails",
               url: "Carts/CartDetails/{CustomerId}",
               defaults: new { controller = "Carts", action = "CartDetails" }
           );



        }
    }
}
