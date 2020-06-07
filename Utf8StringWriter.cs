using System.IO;
using System.Text;

namespace DirectoryPodcastRss
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}