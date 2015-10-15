using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Authentication.Api.Startup))]

namespace Authentication.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            //app.UseCors(CorsOptions.AllowAll);


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);


            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // Token Generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = new SimpleAuthorizationServerProvider()
            });

            // Token verification
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Realm = "Test",
                Provider = new OAuthBearerAuthenticationProvider
                {
                    OnRequestToken = context => Task.FromResult<object>(null),
                    OnValidateIdentity = context => Task.FromResult<object>(null),
                    //OnApplyChallenge = context => Task.FromResult(0),
                }
            });
        }
    }
}
