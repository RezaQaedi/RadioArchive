using System;
using RadioArchive.Core;

namespace RadioArchive
{
    public class PodcastApi
    {
        public string Url { get; set; }
        public string UrlReferer { get; set; }
        public bool IsBestOfTheWeek { get; set; }
        public PodcastTime PodcastTime { get; set; }
        public DateTimeOffset Date { get; set; }

        public PodcastApi(string url, 
            string urlReferer, 
            DateTimeOffset date,
            bool isBestOfTheWeek = false,
            PodcastTime time = PodcastTime.Morning)
        {
            Url = url;
            UrlReferer = urlReferer;
            IsBestOfTheWeek = isBestOfTheWeek;
            PodcastTime = time;
            Date = date;
        }
    }
}
