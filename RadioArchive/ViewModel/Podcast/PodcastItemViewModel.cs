using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace RadioArchive
{
    public class PodcastItemViewModel : BaseViewModel
    {
        /// <summary>
        /// List of Shows
        /// </summary>
        public ObservableCollection<PodcastViewModel> Shows { get; set; }

        /// <summary>
        /// Title of this Podcast
        /// </summary>
        public string DisplayTitle => Date.ToString("dddd");

        /// <summary>
        /// Indicates if this item is new 
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Inidacates if one of this item shows is playing 
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Command for Slelecting this item 
        /// </summary>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        /// The date of relase 
        /// </summary>
        public DateTimeOffset Date
        {
            get
            {
                return Shows[0].Date;
            }
        }

        /// <summary>
        /// Color of background 
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        ///  50% Lighter version of <see cref="BackgroundColor"/>
        /// </summary>
        public Color BackgroundColorLighter => BackgroundColor.Lerp(Colors.White, 0.5f);

        public PodcastItemViewModel(Color backgroundColor)
        {
            // Create a show list 
            Shows = new ObservableCollection<PodcastViewModel>();

            BackgroundColor = backgroundColor;

            SelectCommand = new RelayCommand(() => 
            {
                DI.ViewModelApplication.ShowPlayList(DisplayTitle, Shows);
            });
        }
    }
}
