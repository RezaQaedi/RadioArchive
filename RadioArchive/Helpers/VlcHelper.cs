using Dna;
using LibVLCSharp.Shared;
using System;
using System.IO;
using System.Reflection;
using static Dna.FrameworkDI;

namespace RadioArchive.Core
{
    public static class VlcHelper
    {
        public static Media GetNewMedia(this LibVLC libVLC,DateTimeOffset dateTime, PodcastTime podcastTime,params VLCMediaOptions[] vLCMediaOptions)
        {
            RouteHelper.GetUrlForSpeceficDate(dateTime, podcastTime, out var url, out var urlReferre);

            var media = new Media(libVLC, new Uri(url));

            foreach (var options in vLCMediaOptions)
            {
                switch (options)
                {
                    case VLCMediaOptions.Keep:
                        media.AddOption(":sout-keep");
                        break;
                    case VLCMediaOptions.File:
                        media.AddOption(":sout=#file{dst=" + GetDestenation(dateTime, podcastTime) + "}");
                        break;
                    case VLCMediaOptions.Referrer:
                        media.AddOption($":http-referrer={urlReferre}");
                        break;
                }
            }

            return media;
        }

        private static string GetDestenation(DateTimeOffset dateTime, PodcastTime podcastTime)
        {
            var currentDirectory = Path.GetDirectoryName(AppContext.BaseDirectory);

            // TODO : catch exeption when failing to create directory
            try
            {
                if (!Directory.Exists(Path.Combine(currentDirectory, "Records")))
                    Directory.CreateDirectory(Path.Combine(currentDirectory, "Records"));
            }
            catch (Exception e)
            {
                Logger.LogDebugSource($"Faild to make records directory with error {e.Message}");
            }

            return Path.Combine($"{currentDirectory}\\Records", $"{dateTime:yyy_MM_dd}_{podcastTime}_{DateTime.UtcNow.Ticks}.mp3");
        }
    }
}
