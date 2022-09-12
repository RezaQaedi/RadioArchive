using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using static RadioArchive.DI;

namespace RadioArchive
{
    public class PodcastPlayListViewModel : BaseViewModel
    {
        #region Public methods 
        /// <summary>
        /// Year of this podcastlist 
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Month of podcastlist 
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Title of podcastlist 
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Inidactes if list is getting its items 
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Inidcates failer of loading 
        /// </summary>
        public bool FaildToLoad { get; set; }

        /// <summary>
        /// Inidcates if no item has found 
        /// </summary>
        public bool EmptyList => IsLoading == false && Items.Items.Count == 0;

        /// <summary>
        /// Items of this podcast playlist 
        /// </summary>

        public PodcastListViewModel Items { get; set; }

        /// <summary>
        /// Command for backing up to last shows page 
        /// </summary>

        public ICommand BackCommand { get; set; }

        /// <summary>
        /// Command for retring loading 
        /// </summary>

        public ICommand RetryCommand { get; set; }
        #endregion

        #region Counstractor

        public PodcastPlayListViewModel() { }

        public PodcastPlayListViewModel(int year, int month)
        {
            Year = year;
            Month = month;
            DisplayTitle = $"{TimeHelper.GetInvariantMonthName(month)} ({year})";

            Items = new PodcastListViewModel();

            BackCommand = new RelayCommand(() =>
            {
                var lastShowVM = new LastShowsViewModel();
                lastShowVM.SelectYear(Year);
                ViewModelApplication.GoToPage(ApplicationPage.LastShows, lastShowVM);
            });

            RetryCommand = new RelayCommand(LoadAsync);
        }
        #endregion

        #region Public Methods 

        /// <summary>
        /// Gets items of this podcastlist 
        /// </summary>
        public async void LoadAsync()
        {
            List<PodcastApi> data = null;

            await RunCommand(() => IsLoading, async () =>
            {
                FaildToLoad = false;
                // Try to get data 
                data = await ApiService.GetShowsWithSpecificDateAsync(Year, Month);

            }).ContinueWith(t =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (data != null)
                    {
                        // If we got the data then add it to list 
                        foreach (var podcastURL in data)
                        {
                            ModelHelper.AddPodcastViewModel(podcastURL, Items);
                        }

                        OnPropertyChanged(nameof(EmptyList));
                        return;
                    }

                    // Set the fail flag to true when list is still null
                    FaildToLoad = true;
                });
            });
        } 
        #endregion
    }
}