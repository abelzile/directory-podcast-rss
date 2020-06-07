using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DirectoryPodcastRss
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Options options)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Rss}/{action=Index}");
            });

            PrepareFiles(env, options);
        }

        private static void PrepareFiles(IWebHostEnvironment env, Options options)
        {
            var imageFilePath = options.ImageFilePath;
            var fileDir = options.InputDirectory;
            var workingDirName = HashUtility.ComputeFileMd5Hash(imageFilePath);
            var workingPath = Path.Combine(env.WebRootPath, workingDirName);

            if (Directory.Exists(workingPath))
            {
                Directory.Delete(workingPath, true);
            }

            Directory.CreateDirectory(workingPath);

            var coverPathTmp = Path.Combine(workingPath, "cover.tmp");
            var coverPath = Path.ChangeExtension(coverPathTmp, Path.GetExtension(imageFilePath));

            File.Copy(imageFilePath, coverPath);

            EnsureItunesCompliantImage(coverPath);

            var srcFiles = Directory.GetFiles(fileDir).OrderBy(s => s).ToArray();

            for (var i = 0; i < srcFiles.Length; ++i)
            {
                var mediaPathTmp = Path.Combine(workingPath, $@"file{i}.tmp");
                var mediaPath = Path.ChangeExtension(mediaPathTmp, Path.GetExtension(srcFiles[i]));

                File.Copy(srcFiles[i], mediaPath);
            }
        }

        private static void EnsureItunesCompliantImage(string coverPath)
        {
            using (var img = Image.Load(coverPath))
            {
                var size = img.Size();
                
                if (size.Height < 1400 || size.Width < 1400)
                {
                    img.Mutate(x => x.Resize(1400, 1400));
                    img.Save(coverPath);
                }
                else if (size.Height > 3000 || size.Width > 3000)
                {
                    img.Mutate(x => x.Resize(3000, 3000));
                    img.Save(coverPath);
                }
            }
        }
    }
}