using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TodoApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // This introduces a security risk.
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling =                    
                    Newtonsoft.Json.TypeNameHandling.All;
                    // Newtonsoft.Json.TypeNameHandling.None;

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Web API configuration and services

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
