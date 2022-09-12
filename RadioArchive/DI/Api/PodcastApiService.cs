using Dna;
using HtmlAgilityPack;
using RadioArchive.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Dna.FrameworkDI;

namespace RadioArchive
{
    public class PodcastApiService : IPodcastApiService
    {
        private const string TOPSHOWS = "top-shows/";
        private const string PAGEOFFSET = "?&offset=";
        private const string Archiv = "?blog=HolakoueeArchiv&archive=";
        private readonly HttpClient _httpClient;

        public PodcastApiService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets List of Last <see cref="PodcastApi"/> Show's
        /// </summary>
        /// <returns></returns>
        public async Task<List<PodcastApi>> GetLastShowsAsync()
        {
            return await GetShows(RouteHelper.GetAbsoluteRoute(""));
        }

        /// <summary>
        /// Get Show's list with offset 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<PodcastApi>> GetShowsWithOffsetAsync(string offset)
        {
            return await GetShows($"{RouteHelper.GetAbsoluteRoute(PAGEOFFSET)}{offset}");
        }

        /// <summary>
        /// Get Show's list with offset 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<PodcastApi>> GetShowsWithSpecificDateAsync(int year, int month)
        {
            return await GetShows($"{RouteHelper.GetAbsoluteRoute(Archiv)}{year}-{month}");
        }

        /// <summary>
        /// Gets List of Top rated <see cref="PodcastApi"/> Show's
        /// </summary>
        /// <returns></returns>
        public async Task<List<PodcastApi>> GetTopRatedShowsAsync()
        {
            return await GetShows(RouteHelper.GetAbsoluteRoute(TOPSHOWS));
        }

        /// <summary>
        /// Get request for <paramref name="url"/>
        /// </summary>
        /// <param name="url">Url for get request</param>
        /// <returns>List of <see cref="PodcastApi"/> in <paramref name="url"/></returns>
        private async Task<List<PodcastApi>> GetShows(string url)
        {
            List<PodcastApi> podcastUrlList = null;

            try
            {
                //var html = await _httpClient.GetStringAsync(url).ConfigureAwait(false);
                using var responseMessage = await _httpClient.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();

                var contentStr = await responseMessage.Content.ReadAsStringAsync();

                podcastUrlList = GetShowsList(contentStr);
            }
            catch (Exception ex)
            {
                Logger.LogDebugSource($"Attempted to get data from {url} with [{ex.Message}] Error");
            }


            return podcastUrlList;
        }

        /// <summary>
        /// Get list of <see cref="PodcastApi"/> from a <paramref name="html"/> url
        /// </summary>
        /// <param name="html">Url adrees containg the speceifc format of html items</param>
        /// <returns></returns>
        private static List<PodcastApi> GetShowsList(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var podcastHtml = htmlDocument.DocumentNode.Descendants("h6")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("item")).ToList();

            var podcastUrlList = new List<PodcastApi>();

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
                var time = PodcastTime.None;

                // Determine time and remove extra strings from strDate 
                if (stringItem.Contains("Evening"))
                {
                    time = PodcastTime.Evening;
                    strDate = strDate.Replace("Evening", string.Empty);
                }
                else if (stringItem.Contains("Morning"))
                {
                    time = PodcastTime.Morning;
                    strDate = strDate.Replace("Morning", string.Empty);
                }
                else if (stringItem.Contains("Afternoon"))
                {
                    time = PodcastTime.Afternoon;
                    strDate = strDate.Replace("Afternoon", string.Empty);
                }
                // Best of the week 
                if (stringItem.Contains("(best of week)"))
                {
                    isBestOfTheWeek = true;
                    strDate = strDate.Replace("(best of week)", string.Empty);
                }

                var culture = CultureInfo.InvariantCulture;
                var styles = DateTimeStyles.None;

                var format = "MMMM d, yyyy";

                if (DateTime.TryParseExact(strDate.Trim(), format, culture, styles, out var dateTime))
                {

                    // Get urls  
                    RouteHelper.GetUrlForSpeceficDate(dateTime, time, out var url, out var urlR);

                    podcastUrlList.Add(new PodcastApi(url, urlR, dateTime, isBestOfTheWeek, time));

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
