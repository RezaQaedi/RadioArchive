using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using RadioArchive.Core;

namespace RadioArchive
{
    public class PodcastItemDesignModel : PodcastItemViewModel
    {

        #region Singleton
        /// <summary>
        /// a single instance of design model
        /// </summary>
        public static PodcastItemDesignModel Instance =>
            new PodcastItemDesignModel(Color.FromRgb(255, 183, 58)) { Shows = new ObservableCollection<PodcastViewModel>() { new PodcastViewModel(DateTime.UtcNow, PodcastTime.Morning, Color.FromRgb(255, 183, 58)) } };
        #endregion

        #region Constructor
        public PodcastItemDesignModel(Color backgroundColor) : base(backgroundColor)
        {
        }

        #endregion
    }
}
