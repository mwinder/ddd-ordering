using System.Web.Http;
using Owin;
using Swashbuckle.Application;

namespace Ordering.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            configuration
                .RegisterApi();
            configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "Ordering API"))
                .EnableSwaggerUi();

            app.UseWebApi(configuration);
        }
    }
}