using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ordering.Api.Models
{
    public abstract class Linkable
    {
        protected Linkable() { }

        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        private IDictionary<string, Link> Links { get; set; }

        public Link Link(string rel, string href)
        {
            return Link(new Link(rel, href));
        }

        public Link Link(Link link)
        {
            if (Links == null) Links = new Dictionary<string, Link>();

            return Links[link.Rel] = link;
        }
    }
}