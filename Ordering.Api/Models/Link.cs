using System;

namespace Ordering.Api.Models
{
    public class Link
    {
        public Link(Uri href)
        {
            //Rel = rel;
            Href = href;
        }

        //public string Rel { get; private set; }

        public Uri Href { get; private set; }
    }
}