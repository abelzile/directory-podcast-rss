using System;
using System.Xml.Serialization;

namespace DirectoryPodcastRss.Models
{
    public class MediaContent
    {
        private readonly Enclosure _enclosure;

        public MediaContent(Enclosure enclosure)
        {
            if (enclosure is null)
            {
                throw new System.ArgumentNullException(nameof(enclosure));
            }

            this._enclosure = enclosure;
        }

        public MediaContent()
        {
        }

        [XmlAttribute("url")]
        public string Url
        {
            get => _enclosure.Url;
            set => throw new NotImplementedException();
        }

        [XmlAttribute("fileSize")]
        public long FileSize
        {
            get => _enclosure.Length;
            set => throw new NotImplementedException();
        }

        [XmlAttribute("type")]
        public string Type
        {
            get => _enclosure.Type;
            set => throw new NotImplementedException();
        }
    }
}