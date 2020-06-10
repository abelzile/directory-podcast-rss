using System.IO;
using System.Xml.Serialization;
using DirectoryPodcastRss.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryPodcastRss.Controllers
{
    public partial class RssController : ControllerBase
    {
        private IWebHostEnvironment _env;

        public RssController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index([FromServices] Options options)
        {
            var workingDirName = HashUtility.ComputeFileMd5Hash(options.ImageFilePath);
            var workingPath = Path.Combine(_env.WebRootPath, workingDirName);
            var coverPath = Path.Combine(workingPath, "cover.jpg");
            var serverPath = $"{Request.Scheme}://{Request.Host.Value}/{workingDirName}";
            var rss = new FeedGenerator(workingPath, coverPath, serverPath).Generate();

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SerializeRss(rss),
                StatusCode = 200
            };
        }

        private static string SerializeRss(Rss rss)
        {
            var ns = new XmlSerializerNamespaces();

            foreach (var item in Rss.XmlNamespaces)
            {
                ns.Add(item.Key, item.Value);
            }

            var serializer = new XmlSerializer(rss.GetType());

            using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, rss, ns);
                return writer.ToString();
            }
        }
    }
}