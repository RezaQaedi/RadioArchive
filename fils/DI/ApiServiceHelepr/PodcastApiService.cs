using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RadioArchive
{
    public class PodcastApiService : IPodcastApiService
    {
        private const string TOPSHOWS = "top-shows/";
        private const string PAGEOFFSET = "?&offset=";
        private readonly HttpClient _httpClient;

        public PodcastApiService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets List of Last <see cref="PodcastURL"/> Show's
        /// </summary>
        /// <returns></returns>
        public async Task<List<PodcastURL>> GetLastShowsAsync()
        {
            var html = await _httpClient.GetStringAsync(RouteHelper.GetAbsoluteRoute(""));

            var podcastUrlList = GetShowsList(html);

            return podcastUrlList;
        }

        /// <summary>
        /// Get Show's list with offset 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<PodcastURL>> GetShowsWithOffsetAsync(string offset)
        {
            var html = await _httpClient.GetStringAsync($"{RouteHelper.GetAbsoluteRoute(PAGEOFFSET)}{offset}");

            return GetShowsList(html);
        }

        /// <summary>
        /// Gets List of Top rated <see cref="PodcastURL"/> Show's
        /// </summary>
        /// <returns></returns>
        public async Task<List<PodcastURL>> GetTopRatedShowsAsync()
        {
            // TODO THERE IS NO ERROR HANDELING 
            var html = await _httpClient.GetStringAsync(RouteHelper.GetAbsoluteRoute(TOPSHOWS));

            var podcastUrlList = GetShowsList(html);

            return podcastUrlList;
        }
        
        /// <summary>
        /// Get list of <see cref="PodcastURL"/> from a <paramref name="html"/> url
        /// </summary>
        /// <param name="html">Url adrees containg the speceifc format of html items</param>
        /// <returns></returns>
        private List<PodcastURL> GetShowsList(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var podcastHtml = htmlDocument.DocumentNode.Descendants("h6")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("item")).ToList();

            var podcastUrlList = new List<PodcastURL>();

            foreach (var items in podcastHtml)
            {
                var stringItem = items.SelectSingleNode("a").InnerText
                    .Replace("Listen to", "")
                    .Trim();

                // Make sure we have something to work with 
                if (string.IsNullOrEmpty(stringItem))
                    continue;

                // Date text 
                var strDate = stringItem;
                var isBestOfTheWeek = false;
                var time = PodcastTime.none;

                // Determine time and remove extra strings from strDate 
                if (stringItem.Contains("Evening"))
                {
                    time = PodcastTime.Evening;
                    strDate = strDate.Replace("Evening", "");
                }
                else if (stringItem.Contains("Morning"))
                {
                    time = PodcastTime.Morning;
                    strDate = strDate.Replace("Morning", "");
                }

                // Best of the week 
                if (stringItem.Contains("(best of week)"))
                {
                    isBestOfTheWeek = true;
                    strDate = strDate.Replace("(best of week)", "");
                }

                var culture = CultureInfo.InvariantCulture;
                var styles = DateTimeStyles.None;

                var format = "MMMM d, yyyy";

                if (DateTime.TryParseExact(strDate.Trim(), format, culture, styles, out var dateTime))
                {
                    // pars to date time 
                    //DateTimeOffset.TryParse(strDate, out var dateTime);

                    // Get urls  
                    RouteHelper.GetUrlForSpeceficDate(dateTime, time, out var url, out var urlR);

                    podcastUrlList.Add(new PodcastURL(url, urlR, dateTime, isBestOfTheWeek, time));

                    // Link 
                    //var a = items.Descendants("a").FirstOrDefault().GetAttributes("href", "");
                }
                else
                    continue;
            }

            return podcastUrlList;
        }
    }
}
