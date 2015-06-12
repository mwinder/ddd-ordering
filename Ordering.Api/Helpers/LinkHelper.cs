using System;
using System.Web.Http.Routing;

namespace Ordering.Api.Helpers
{
    public static class LinkHelper
    {
        public static Uri Link(this UrlHelper url, object values)
        {
            return new Uri(url.Link("DefaultApi", values));
        }
    }
}