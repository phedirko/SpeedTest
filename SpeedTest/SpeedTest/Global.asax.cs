using SpeedTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SpeedTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            using(var ctx = new STdbcontext())
            {
                ctx.Set<Site>().Add(new Site
                {
                    Address = "adads",
                    Measurements = new List<Measurement>(),
                    Urls = new List<Url>()
                });
            }
        }
    }
}
