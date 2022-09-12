using System;
using System.Globalization;

namespace RadioArchive
{
    public static class RouteHelper
    {
        private const string HOST = "https://www.holakoueearchive.co";
        private const string GETFILE = "mp3/slave/w_";
        private const string GETFILEREFERRER = "listen-to-show.php?&str=";

        public static void GetUrlForSpeceficDate(DateTimeOffset date, PodcastTime time, out string fileUrl, out string fileUrlReferrer)
        {
            var strDate = date.ToString("yyyyddMM", CultureInfo.InvariantCulture);
            var strDateMonth = date.ToString("MMMM", CultureInfo.InvariantCulture);
            var isEvening = time == PodcastTime.Evening ? "e" : "m";
            if (time == PodcastTime.none)
                isEvening = "";

            fileUrl = $"{GetAbsoluteRoute(GETFILE)}{strDate}{isEvening}.mp3";
            fileUrlReferrer = $"{GetAbsoluteRoute(GETFILEREFERRER)}{strDate}{isEvening}&f={strDateMonth}";
        }

        /// <summary>
        /// Converts a relative URL into an absolute URL
        /// </summary>
        /// <param name="relativeUrl">The relative URL</param>
        /// <returns>Returns the absolute URL including the Host URL</returns>
        public static string GetAbsoluteRoute(string relativeUrl)
        {
            // Get the host
            var host = HOST;

            // If they are not passing any URL...
            if (string.IsNullOrEmpty(relativeUrl))
                // Return the host
                return host;

            // If the relative URL does not start with /...
            if (!relativeUrl.StartsWith("/"))
                // Add the /
                relativeUrl = $"/{relativeUrl}";

            // Return combined URL
            return host + relativeUrl;
        }
    }
}
