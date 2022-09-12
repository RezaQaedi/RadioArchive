using System;
using System.Windows.Input;

namespace RadioArchive
{
    public class PodcastViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// Get or set podcast time 
        /// </summary>
        public PodcastTime Time { get; set; }

        /// <summary>
        /// Indicate's this podcast is replay show 
        /// </summary>
        public bool IsReplay { get; set; }

        /// <summary>
        /// Get's if this podcast dosnt have any specific date 
        /// </summary>
        public bool NoSpecificDate => Time == PodcastTime.none;

        /// <summary>
        /// The date of relase 
        /// </summary>
        public DateTimeOffset Date { get; set; }
        #endregion

        #region Commands

        public ICommand Play { get; set; }

        #endregion
        public PodcastViewModel(DateTimeOffset date, PodcastTime podcastTime, bool isReplay = false)
        {
            Time = podcastTime;
            Date = date;
            IsReplay = isReplay;

            Play = new RelayCommand(() => DI.ViewModelPodcastPlayer.OpenPodcast(this.ToPodcastUrl(), true));
        }
    }
}
