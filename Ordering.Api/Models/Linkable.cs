using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Ordering.Api.Models
{
    public abstract class Linkable
    {
        protected Linkable()
        {
            Links = new Collection<Link>();
        }

        [JsonProperty("_links")]
        public ICollection<Link> Links { get; set; }
    }
}