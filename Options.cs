using CommandLine;

namespace DirectoryPodcastRss
{
    public class Options
    {
        [Option('d', "dir", Required = true, HelpText = "Directory containing mp3 files to add to feed.")]
        public string InputDirectory { get; set; }

        [Option('i', "imgpath", Required = true, HelpText = "Path to feed image.")]
        public string ImageFilePath { get; set; }
    }
}

