
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace refactor_me
{
    /// <summary>
    /// The base class that provides the entry point to the system.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The entry point to the code.
        /// </summary>
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
