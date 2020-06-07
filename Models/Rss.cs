using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    [XmlRoot("rss")]
    public class Rss
    {
        public const string ItunesNamespace = "http://www.itunes.com/dtds/podcast-1.0.dtd";
        public const string ContentNamespace = "http://purl.org/rss/1.0/modules/content/";
        public const string GooglePlayNamespace = "https://www.google.com/schemas/play-podcasts/1.0/";
        public const string MediaNamespace = "http://search.yahoo.com/mrss/";

        public static readonly IDictionary<string, string> XmlNamespaces = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            { "itunes", ItunesNamespace },
            { "content", ContentNamespace },
            { "googleplay", GooglePlayNamespace },
            { "media", MediaNamespace }
        });

        [XmlAttribute("version")]
        public string Version { get; set; } = "2.0";
        [XmlElement("channel")]
        public Channel Channel { get; set; }
    }
}