using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using IO = System.IO;
using Model = DirectoryPodcastRss.Models;

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
            var workingPath = IO.Path.Combine(_env.WebRootPath, workingDirName);
            var coverPath = IO.Path.Combine(workingPath, "cover.jpg");
            var serverPath = $"{Request.Scheme}://{Request.Host.Value}/{workingDirName}";
            var feedGenerator = new FeedGenerator(workingPath, coverPath, serverPath);
            var rss = feedGenerator.Generate();

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = SerializeRss(rss),
                StatusCode = 200
            };
        }

        private static string SerializeRss(Model.Rss rss)
        {
            var ns = new XmlSerializerNamespaces();

            foreach (var item in Model.Rss.XmlNamespaces)
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