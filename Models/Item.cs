using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class Item
    {
        private DateTime _pubDate;

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("title", Namespace = Rss.ItunesNamespace)]
        public string ItunesTitle { get { return Title; } }

        [XmlElement("episodeType", Namespace = Rss.ItunesNamespace)]
        public string ItunesEpisodeType { get; set; } = "full";

        [XmlElement("episode", Namespace = Rss.ItunesNamespace)]
        public string ItunesEpisode { get; set; }

        [XmlElement("summary", Namespace = Rss.ItunesNamespace)]
        public string ItunesSummary { get; set; }

        [XmlElement("encoded", Namespace = Rss.ContentNamespace)]
        public string ContentEncoded { get; set; }

        [XmlElement("guid")]
        public RssGuid Guid { get; set; }

        [XmlIgnore]
        public DateTime PubDate
        {
            get { return _pubDate; }
            set
            {
                if (value.Kind != DateTimeKind.Utc)
                {
                    throw new FormatException("Please specifiy PubDate in UTC.");
                }
                _pubDate = value;
            }
        }

        [XmlElement("pubDate")]
        public string PubDateAsRfc2822
        {
            get
            {
                //TODO: make better.
                return PubDate.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")) + " -0000";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [XmlElement("explicit", Namespace = Rss.ItunesNamespace)]
        public bool ItunesExplicit { get; set; }

        [XmlElement("image", Namespace = Rss.ItunesNamespace)]
        public ItunesImage ItunesImage { get; set; }

        public List<string> ItunesKeywords { get; set; }

        [XmlIgnore]
        public TimeSpan ItunesDuration { get; set; }

        [XmlElement("duration", Namespace = Rss.ItunesNamespace)]
        public double ItunesDurationSeconds
        {
            get { return ItunesDuration.TotalSeconds; }
            set { ItunesDuration = TimeSpan.FromSeconds(value); }
        }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("content", Namespace = Rss.MediaNamespace)]
        public MediaContent MediaContent { get; set; }

        [XmlElement("subtitle", Namespace = Rss.ItunesNamespace)]
        public string ItunesSubtitle { get; set; }

        [XmlElement("author", Namespace = Rss.ItunesNamespace)]
        public string ItunesAuthor { get { return Author; } }

        [XmlElement("enclosure")]
        public Enclosure Enclosure { get; set; }
    }
}
