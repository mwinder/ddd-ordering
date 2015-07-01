using System;
using System.Web.Http.Routing;

namespace Ordering.Api.Helpers
{
    public static class LinkHelper
    {
        public static Uri LinkUri(this UrlHelper url, object values, string routeName = "DefaultApi")
        {
            return new Uri(url.Link(routeName, values));
        }
    }
}