using RadioArchive.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadioArchive
{
    /// <summary>
    /// the application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {

        #region Public Properties

        public ICommand GoHomePageCommand { get; set; }

        public ICommand GoLastShowsPageCommand { get; set; }

        public ICommand GoPlayListPageCommand { get; set; }

        /// <summary>
        /// the current page of application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        /// <summary>
        /// The view model to use for the current page when current page changes
        /// NOTE : this is not the Live Up To Date view model for the current page 
        ///        its simply used to set the view model of the current page at the
        ///        time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// List of media items
        /// </summary>
        public PlaylistViewModel PlayList { get; set; }

        /// <summary>
        /// True if side menu should be visible
        /// </summary>
        public bool SideMenuVisible { get; set; } = true;

        /// <summary>
        /// True if play list should be visible 
        /// </summary>
        public bool PlayListVisible { get; set; }

        #endregion

        #region Constructor
        public ApplicationViewModel()
        {
            PlayList = new();

            // Creating commands 
            GoHomePageCommand = new RelayCommand(() => GoToPage(ApplicationPage.Home, new HomeViewModel()));
            GoLastShowsPageCommand = new RelayCommand(() => GoToPage(ApplicationPage.LastShows, new LastShowsViewModel()));
            GoPlayListPageCommand = new RelayCommand(() => GoToPage(ApplicationPage.UserPlayList, new UserPlayListViewModel()));

            // Events 
            DI.ViewModelPodcastPlayer.PodcastOpend += NewMediaOpend;
        }

        private void NewMediaOpend(PodcastViewModel vm)
        {
            DI.StorgeService.UpdateVisitDate(vm);
        }
        #endregion

        #region Public Methods 

        /// <summary>
        /// navigates to specified page
        /// </summary>
        /// <param name="viewModel">The view model if any, set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Hide PlayList on navigation 
            PlayListVisible = false;

            //set the current page
            CurrentPage = page;

            // Set the view model
            CurrentPageViewModel = viewModel;

            // Fire off a current page changed event
            OnPropertyChanged(nameof(CurrentPage));
        }

        #region Playlist methods 

        public void ShowPlayList(string title, IEnumerable<ShowDataModel> dataModels, bool isRemovble = false)
        {

            var vms = new List<PodcastViewModel>();

            if(dataModels != null)
                foreach (var item in dataModels)
                {
                    var vm = item.ToViewModel();
                    vms.Add(vm);
                }

            PlayList.DisplayTitle = title;
            PlayList.AddItems(vms, isRemovble);
            PlayListVisible = true;
        }

        public void ShowPlayList(string title, IEnumerable<PodcastViewModel> viewModels, bool isRemovble = false)
        {
            PlayList.DisplayTitle = title;
            PlayList.AddItems(viewModels, isRemovble);
            PlayListVisible = true;
        }

        public void ShowUserCreatedList(string key)
        {
            var list = DI.StorgeService.GetUserCreatedList(key);
            ShowPlayList(list.Title, list.Shows, true);
        }

        public void ShowLikedList() =>
            ShowPlayList("Best for you", DI.StorgeService.GetLikedShows());

        public void ShowUserHistoryList() =>
            ShowPlayList("History", DI.StorgeService.GetVisitedShows());

        /// <summary>
        /// Remove specefic podcast from main list 
        /// </summary>
        /// <param name="podcastViewModel">view Model to remove</param>
        public async void RemovePodcast(PodcastViewModel podcastViewModel)
        {
            await RunCommand(() => Working, async () =>
            {
                await Task.Run(() => DI.StorgeService.RemoveShowFromPlayList(podcastViewModel, PlayList.DisplayTitle));
            });

            PlayList.Items.Remove(podcastViewModel);
        }

        #endregion

        #endregion
    }
}
