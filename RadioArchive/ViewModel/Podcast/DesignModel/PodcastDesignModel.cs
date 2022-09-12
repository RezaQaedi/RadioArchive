using System;
using System.Windows.Media;
using RadioArchive.Core;

namespace RadioArchive
{
    public class PodcastDesignModel : PodcastViewModel
    {
        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastDesignModel Instance =>
            new PodcastDesignModel(DateTimeOffset.UtcNow, PodcastTime.Morning, ColorHelper.GetRandomColor(), true);
        #endregion

        #region Constructor
        public PodcastDesignModel(DateTimeOffset date, PodcastTime podcastTime, Color BackgroundColor, bool isReplay = false) : base(date, podcastTime, BackgroundColor, isReplay)
        {
        }
        #endregion
    }
}
