using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ordering.Api.Models
{
    public abstract class Linkable
    {
        protected Linkable()
        {
            Links = new Dictionary<string, Link>();
        }

        [JsonProperty("_links")]
        private IDictionary<string, Link> Links { get; set; }

        public Link Link(string rel, string href)
        {
            return Links[rel] = new Link(href);
        }
    }
}