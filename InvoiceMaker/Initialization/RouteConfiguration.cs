using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace InvoiceMaker.Initialization
{
    public class RouteConfiguration
    {
        public static void AddRoutes(RouteCollection routes)
        {
            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new
            {
                controller = "Invoices",
                action = "Index",
                id = UrlParameter.Optional
            }
);
        }
    }
}