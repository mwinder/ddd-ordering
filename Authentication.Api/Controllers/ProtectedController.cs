using System.Web.Http;

namespace Authentication.Api.Controllers
{
    public class ProtectedController : ApiController
    {
        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
