using System.Collections.Generic;
using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class Category
    {
        [XmlAttribute("text")]
        public string Text { get; set; }

        [XmlElement("category", Namespace = Rss.ItunesNamespace)]
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}