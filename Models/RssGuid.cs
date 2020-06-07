using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class RssGuid
    {
        [XmlAttribute("isPermaLink")]
        public bool IsPermalink { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}