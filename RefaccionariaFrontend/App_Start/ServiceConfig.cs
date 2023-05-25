using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc;
using RefaccionariaFrontend.Interfaces;
using RefaccionariaFrontend.Services;
using System.Collections.Generic;
using System;

namespace MyApp
{
    public static class ServiceConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }

    public class MyDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public MyDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _serviceProvider.GetServices(serviceType);
        }
    }
}
