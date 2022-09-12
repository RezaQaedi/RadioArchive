using System;

namespace RadioArchive
{
    public class PodcastDesignModel : PodcastViewModel
    {
        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastDesignModel Instance => 
            new PodcastDesignModel(DateTimeOffset.UtcNow, PodcastTime.Morning, true);
        #endregion

        #region Constructor
        public PodcastDesignModel(DateTimeOffset date, PodcastTime podcastTime, bool isReplay = false) : base(date, podcastTime, isReplay)
        {
        }
        #endregion
    }
}
