using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shiftim
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "WorkerPanel",
            url: "workerpanel",
            defaults: new { controller = "Home", action = "WorkerPanel", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "ManagerPanel",
            url: "ManagerPanel",
            defaults: new { controller = "Manager", action = "ManagerPanel", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
