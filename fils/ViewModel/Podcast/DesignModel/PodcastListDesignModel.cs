using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace RadioArchive
{
    public class PodcastListDesignModel : PodcastListViewModel
    {
        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastListDesignModel Instance => new PodcastListDesignModel();
        #endregion

        #region Constructor

        public PodcastListDesignModel()
        {
            var backgroundcolor = Color.FromRgb(255, 183, 58);
            var date = DateTime.UtcNow;
            var morning = new PodcastViewModel(date, PodcastTime.Morning);
            var evening = new PodcastViewModel(date, PodcastTime.Morning);
            var podcastVM = new PodcastItemViewModel(backgroundcolor, morning, evening);

            // Temp fake data

            Items = new ObservableCollection<PodcastItemViewModel>
            {
                podcastVM,
                podcastVM,
                podcastVM,
                podcastVM,
                podcastVM,
            };
        }
    }

    #endregion
}
