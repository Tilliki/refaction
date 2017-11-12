using Microsoft.Practices.Unity;
using refactor_me.Repositories;
using refactor_me.Services;
using System.Web.Http;
using Unity.WebApi;

namespace refactor_me
{
    /// <summary>
    /// Configures Unity for dependency injection.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Register all components you need dependency injection on.
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IProductsService, ProductsService>();
            container.RegisterType<IProductsRepository, ProductsRepository>();
            container.RegisterType<IProductOptionsRepository, ProductOptionsRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}