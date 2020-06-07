using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class Enclosure
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("length")]
        public long Length { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}