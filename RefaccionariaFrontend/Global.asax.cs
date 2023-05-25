using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Services;

namespace RefaccionariaFrontend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
           /* var services = new ServiceCollection();
            ServiceConfig.Configure(services);
            DependencyResolver.SetResolver(new MyDependencyResolver(services.BuildServiceProvider())); */
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
