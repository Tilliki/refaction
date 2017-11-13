using System.Configuration;
using System.Web.Http;
using Microsoft.Practices.Unity;
using refactor_me.Repositories;
using refactor_me.Services;
using Unity.WebApi;

namespace refactor_me
{
    /// <summary>
    ///     Configures Unity for dependency injection.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        ///     Register all components you need dependency injection on.
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IProductsService, ProductsService>();

            int dataPoolSize;
            if (!int.TryParse(ConfigurationManager.AppSettings["DataPoolSize"], out dataPoolSize))
            {
                throw new ConfigurationErrorsException("Configuration setting DataPoolSize is not an integer.");
            }
            container.RegisterInstance<IProductsRepository>(new ProductsRepository(dataPoolSize));
            container.RegisterInstance<IProductOptionsRepository>(new ProductOptionsRepository(dataPoolSize));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}