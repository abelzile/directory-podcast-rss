using System;
using System.IO;
using CommandLine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DirectoryPodcastRss
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            new Parser(settings =>
                {
                    settings.IgnoreUnknownArguments = true;
                    settings.HelpWriter = Console.Out;
                    settings.EnableDashDash = true;
                })
                .ParseArguments<Options>(args)
                .WithParsed<Options>(o => 
                {
                    var imageFilePath = o.ImageFilePath;
                    var fileDir = o.InputDirectory;

                    if (!File.Exists(imageFilePath))
                    {
                        throw new ArgumentException($@"Album cover not found at '{imageFilePath}'.", nameof(o.ImageFilePath));
                    }

                    if (!Directory.Exists(fileDir))
                    {
                        throw new ArgumentException($@"Directory not found at '{fileDir}'.", nameof(o.InputDirectory));
                    }

                    CreateHostBuilder(args, o).Build().Run();
                });
        }

        public static IHostBuilder CreateHostBuilder(string[] args, Options options) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(s => s.AddSingleton(options))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
