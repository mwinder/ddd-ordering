using Microsoft.Owin;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ClaimsUsage
{
    public class AuthenticationMiddleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            var authentication = context.Request.Headers.Get("Authorization");
            if (!string.IsNullOrWhiteSpace(authentication))
            {
                var auth = AuthenticationHeaderValue.Parse(authentication);
            }

            var claims = new[] {
                new Claim(ClaimTypes.Name, "Andy User"),
                new Claim(ClaimTypes.GivenName, "Andy"),
                new Claim(ClaimTypes.Surname, "User"),
                new Claim(ClaimTypes.Email, "auser@example.com"),
                //new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "Manager"),
                new Claim("http://example.com/claims/username", "auser"),
            };
            var identity = new ClaimsIdentity(claims, "Anything");//, "Token", "http://example.com/claims/username", null);
            var principal = new ClaimsPrincipal(identity);
            context.Request.User = principal;

            await Next.Invoke(context);
        }
    }
}