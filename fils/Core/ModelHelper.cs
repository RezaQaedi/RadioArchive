using System.Windows.Media;

namespace RadioArchive
{
    public static class ModelHelper
    {
        /// <summary>
        /// Create's <see cref="PodcastItemViewModel"/> from a <see cref="PodcastURL"/>
        /// </summary>
        /// <param name="podcast">Podcast api model</param>
        /// <param name="background">Color of background</param>
        /// <returns></returns>
        public static PodcastItemViewModel ToPodcastItemViewModel(this PodcastURL podcast, Color background)
        {

            var show = podcast.ToPodcastViewModel();

            // If show has not specifid time or its morning we pass as morning show 
            return show.Time == PodcastTime.Morning || show.Time == PodcastTime.none ? 
                new PodcastItemViewModel(background, show) :
                new PodcastItemViewModel(background, evening:show);
        }

        /// <summary>
        /// Create's <see cref="PodcastItemViewModel"/> from a <see cref="PodcastURL"/>
        /// </summary>
        /// <param name="podcast">Podcast api model</param>
        /// <returns></returns>
        public static PodcastViewModel ToPodcastViewModel(this PodcastURL podcast)
        {
            return new PodcastViewModel(podcast.Date, podcast.PodcastTime, podcast.IsBestOfTheWeek);
        }

        /// <summary>
        /// Create's <see cref="PodcastURL"/> from a <see cref="PodcastViewModel"/>
        /// </summary>
        /// <returns></returns>
        public static PodcastURL ToPodcastUrl(this PodcastViewModel podcastView)
        {
            // Get urls 
            RouteHelper.GetUrlForSpeceficDate(podcastView.Date, podcastView.Time, out var filurl, out var fileUrlReferrer);

            return new PodcastURL(filurl, fileUrlReferrer, podcastView.Date, podcastView.IsReplay, podcastView.Time);
        }
    }
}
