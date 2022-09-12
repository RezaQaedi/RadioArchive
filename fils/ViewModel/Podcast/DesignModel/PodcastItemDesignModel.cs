using System;
using System.Windows.Media;

namespace RadioArchive
{
    public class PodcastItemDesignModel : PodcastItemViewModel
    {

        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastItemDesignModel Instance =>
            new PodcastItemDesignModel(Color.FromRgb(255, 183, 58), new PodcastViewModel(DateTime.UtcNow, PodcastTime.Morning), new PodcastViewModel(DateTime.UtcNow, PodcastTime.Evening));
        #endregion

        #region Constructor
        public PodcastItemDesignModel(Color backgroundColor, PodcastViewModel morningShow, PodcastViewModel evningShow) : base(backgroundColor, morningShow, evningShow)
        {
        }

        #endregion
    }
}
