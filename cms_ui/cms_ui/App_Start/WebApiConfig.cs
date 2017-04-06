using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace cms_ui
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonFormatter = config.Formatters.JsonFormatter;
            if (jsonFormatter != null)
            {
                config.Formatters.Clear();
                config.Formatters.Add(jsonFormatter);
            }

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
