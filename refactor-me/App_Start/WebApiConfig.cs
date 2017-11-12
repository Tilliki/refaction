using System.Web.Http;

namespace refactor_me
{
    /// <summary>
    /// Configures the basic routing for the web service.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the routes with the http configuration.
        /// </summary>
        /// <param name="config">The HttpServer configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
