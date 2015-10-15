using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace ClaimsUsage.Controllers
{
    public class ClaimsController : ApiController
    {
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Get()
        {
            var isAdmin = User.Identity.IsAdmin();

            var claims = User.Identity.Claims().Claims.Select(a => new { subject = a.Subject.Name, type = a.Type, value = a.Value });

            return Ok(claims);
        }
    }

    public static class IdentityExtensions
    {
        public static ClaimsIdentity Claims(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity);
        }

        public static bool HasRole(this IIdentity identity, string role)
        {
            var claimsIdentity = identity.Claims();
            return claimsIdentity.HasClaim(claimsIdentity.RoleClaimType, role);
        }

        public static bool IsAdmin(this IIdentity identity)
        {
            return identity.HasRole("Admin");
        }

        public static bool IsManager(this IIdentity identity)
        {
            return identity.HasRole("Manager");
        }
    }
}
