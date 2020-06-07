using System.Collections.Generic;
using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class Channel
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("managingEditor")]
        public string ManagingEditor { get; set; }

        [XmlElement("copyright")]
        public string Copyright { get; set; }

        [XmlElement("generator")]
        public string Generator { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("author", Namespace = Rss.ItunesNamespace)]
        public string ItunesAuthor { get; set; }

        [XmlElement("summary", Namespace = Rss.ItunesNamespace)]
        public string ItunesSummary { get; set; }

        [XmlElement("language")]
        public string Language { get; set; } = "en";

        [XmlElement("explicit", Namespace = Rss.ItunesNamespace)]
        public bool ItunesExplicit { get; set; }

        [XmlElement("category", Namespace = Rss.ItunesNamespace)]
        public List<Category> ItunesCategories { get; set; } = new List<Category>();

        public List<string> ItunesKeywords { get; set; }

        [XmlElement("type", Namespace = Rss.ItunesNamespace)]
        public string ItunesType { get; set; } = "serial";

        [XmlElement("image", Namespace = Rss.ItunesNamespace)]
        public ItunesImage ItunesImageHref { get; set; }

        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlElement("copyright", Namespace = Rss.MediaNamespace)]
        public string MediaCopyright { get { return Copyright; } }

        [XmlElement("thumbnail", Namespace = Rss.MediaNamespace)]
        public MediaThumbnail MediaThumbnail { get; set; }

        public List<string> MediaKeywords { get { return ItunesKeywords; } }

        [XmlElement("category", Namespace = Rss.MediaNamespace)]
        public List<MediaCategory> MediaCategories { get; set; }

        [XmlElement("owner", Namespace = Rss.ItunesNamespace)]
        public ItunesOwner ItunesOwner { get; set; }

        [XmlElement("subtitle", Namespace = Rss.ItunesNamespace)]
        public string ItunesSubtitle { get; set; }

        [XmlElement("item")]
        public List<Item> Items { get; } = new List<Item>();
    }
}
