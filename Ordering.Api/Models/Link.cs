using Newtonsoft.Json;

namespace Ordering.Api.Models
{
    public class Link
    {
        public Link(
            string href,
            bool templated = false,
            string type = null,
            string name = null,
            string profile = null,
            string title = null)
        {
            Href = href;
            Templated = templated;
            Type = type;
            Name = name;
            Profile = profile;
            Title = title;
        }

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