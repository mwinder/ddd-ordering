using System;
using Newtonsoft.Json;

namespace Ordering.Api.Models
{
    public class Link
    {
        public Link(Uri href, bool templated = false)
        {
            //Rel = rel;
            Href = href;
            Templated = templated;
        }

        //public string Rel { get; private set; }

        public Uri Href { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Templated { get; private set; }
    }
}