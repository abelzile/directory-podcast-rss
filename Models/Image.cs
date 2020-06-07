using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class Image
    {
        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}