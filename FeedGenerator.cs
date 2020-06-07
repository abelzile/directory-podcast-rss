using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DirectoryPodcastRss.Models;

namespace DirectoryPodcastRss
{
    public class FeedGenerator
    {
        public string FileDirectory { get; }
        public string ImagePath { get; }
        public string ServerPath { get; }

        public FeedGenerator(string fileDirectory, string imagePath, string serverPath)
        {
            if (string.IsNullOrWhiteSpace(fileDirectory))
            {
                throw new ArgumentException($"'{nameof(fileDirectory)}' cannot be null or whitespace", nameof(fileDirectory));
            }

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                throw new ArgumentException($"'{nameof(imagePath)}' cannot be null or whitespace", nameof(imagePath));
            }

            if (string.IsNullOrWhiteSpace(serverPath))
            {
                throw new ArgumentException($"'{nameof(serverPath)}' cannot be null or whitespace", nameof(serverPath));
            }

            FileDirectory = fileDirectory;
            ImagePath = imagePath;
            ServerPath = serverPath;
        }

        public Rss Generate()
        {
            var paths = System.IO.Directory.EnumerateFiles(FileDirectory);
            var files = new List<TagLib.File>();

            foreach (var path in paths)
            {
                var tFile = TagLib.File.Create(path);

                if (tFile.Tag.IsEmpty)
                {
                    continue;
                }

                files.Add(tFile);
            }

            var orderedFiles = files.OrderBy(f => f.Tag.Track);

            if (!orderedFiles.Any())
            {
                return null;
            }

            var firstFile = orderedFiles.First();

            var rss = new Rss();
            rss.Channel = new Channel()
            {
                Title = firstFile.Tag.Album,
                Description = firstFile.Tag.Album,
                ItunesAuthor = firstFile.Tag.JoinedPerformers,
                ItunesImageHref = new ItunesImage { Href = ImagePath.Replace(FileDirectory, ServerPath) },
                Image = new Image { Url = ImagePath.Replace(FileDirectory, ServerPath), Title = firstFile.Tag.Album },
            };
            rss.Channel.ItunesCategories.Add(new Category { Text = "Music" });
            rss.Channel.ItunesCategories[0].Categories.Add(new Category { Text = "Music Commentary" });

            var utcNow = DateTime.UtcNow;
            var pubDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, DateTimeKind.Utc);

            foreach (var tFile in orderedFiles)
            {
                var size = new FileInfo(tFile.FileAbstraction.Name).Length;

                var item = new Item()
                {
                    Title = tFile.Tag.Title,
                    Description = tFile.Tag.Title,
                    ItunesEpisode = tFile.Tag.Track.ToString(),
                    Guid = new RssGuid { Text = HashUtility.ComputeFileMd5Hash(tFile.FileAbstraction.Name) },
                    PubDate = pubDate,
                    ItunesDuration = tFile.Properties.Duration,
                    Author = tFile.Tag.JoinedPerformers,
                    ItunesImage = new ItunesImage { Href = ImagePath.Replace(FileDirectory, ServerPath) },
                    Enclosure = new Enclosure
                    {
                        Url = tFile.FileAbstraction.Name.Replace(FileDirectory, ServerPath),
                        Length = size,
                        Type = "audio/mpeg" //tFile.MimeType incorrect
                    }
                };

                rss.Channel.Items.Add(item);

                pubDate = pubDate.AddMinutes(1);
            }

            return rss;
        }
    }
}
