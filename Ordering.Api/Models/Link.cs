using Newtonsoft.Json;

namespace Ordering.Api.Models
{
    public class Link
    {
        public Link(
            string rel,
            string href,
            bool templated = false,
            string type = null,
            string name = null,
            string profile = null,
            string title = null)
        {
            Rel = rel;
            Href = href;
            Templated = templated;
            Type = type;
            Name = name;
            Profile = profile;
            Title = title;
        }

        [JsonIgnore]
        public string Rel { get; private set; }

        public string Href { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Templated { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Profile { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }
    }
}