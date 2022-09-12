using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RadioArchive
{
    public class PodcastListViewModel : BaseViewModel
    {
        #region Public properties 
        /// <summary>
        /// Title of this List
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Indicates this list is shorter version of another 
        /// </summary>
        public bool HasMoreContent { get; set; }

        /// <summary>
        /// Command for navigation to another page 
        /// </summary>
        public ICommand MoreCommand { get; set; }

        /// <summary>
        /// Observable list of <see cref="PodcastItemViewModel"/>
        /// </summary>
        public ObservableCollection<PodcastItemViewModel> Items { get; set; } = new ObservableCollection<PodcastItemViewModel>(); 
        #endregion

        public PodcastListViewModel()
        {
            MoreCommand = new RelayCommand(() => 
            {
                // Navigate to other page or other content 
            });
        }
    }
}
