using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class MediaCategory
    {
        [XmlAttribute("scheme")]
        public string Scheme { get; set; }
        public string Text { get; set; }
    }
}