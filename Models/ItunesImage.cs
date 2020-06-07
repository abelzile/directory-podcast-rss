using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class ItunesImage
    {
        [XmlAttribute("href")]
        public string Href { get; set; }
    }
}