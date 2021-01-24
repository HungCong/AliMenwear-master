using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace project_sem_3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "my order",
               url: "{controller}/{action}/{email}",
               defaults: new { controller = "Account", action = "MyOrder", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ordering",
               url: "{controller}/{action}/{email}",
               defaults: new { controller = "Account", action = "Ordering", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "order detail",
               url: "chi-tiet-don-hang/{order_id}",
               defaults: new { controller = "Account", action = "OrderDetail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
