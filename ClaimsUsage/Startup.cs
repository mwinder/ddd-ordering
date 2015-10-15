using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(ClaimsUsage.Startup))]

namespace ClaimsUsage
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<AuthenticationMiddleware>();

            var configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var serialisationSettings = configuration.Formatters.JsonFormatter.SerializerSettings;
            serialisationSettings.Formatting = Formatting.Indented;
            serialisationSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseWebApi(configuration);
        }
    }
}
