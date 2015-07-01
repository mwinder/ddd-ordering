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
        public IDictionary<string, Link> Links { get; private set; }
    }
}