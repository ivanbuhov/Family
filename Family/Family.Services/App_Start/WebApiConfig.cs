using Family.Services.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Family.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Add filter for the special FamilyException
            config.Filters.Add(new FamilyExceptionFilterAttribute());

            // Change the names of the properties of the outputed JSON to be camelCase
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Uncomment this for adding indentation to the ouptuted JSON
            // settings.Formatting = Formatting.Indented;

            // Web API routes

            config.Routes.MapHttpRoute(
                name: "PeopleApi",
                routeTemplate: "api/People/{action}/{id}",
                defaults: new { controller = "People" });

            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/Users/{action}",
                defaults: new { controller = "users" });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
        }
    }
}
