using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using static RadioArchive.DI;

namespace RadioArchive
{
    public class HomeViewModel : BaseViewModel
    {
        public PodcastListViewModel MostRecentShowsList { get; set; }
        public PodcastListViewModel HighRatedShowList { get; set; }

        public PodcastItemViewModel SelectedItem { get; set; }

        public event Action<HomeViewModel> DataLoaded;

        public bool IsLoading { get; set; }

        public bool FaildToLoad { get; set; }

        public ICommand RetryCommand { get; set; }

        public HomeViewModel()
        {

            // Create view mdoels
            MostRecentShowsList = new PodcastListViewModel() { DisplayTitle = "Most recent shows", HasMoreContent = true };
            HighRatedShowList = new PodcastListViewModel() { DisplayTitle = "Top rated shows" };
            RetryCommand = new RelayCommand(Load);
#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

#endif

            Load();

        }

        public async void Load()
        {
            List<PodcastApi> lastShows = null;
            List<PodcastApi> topShows = null;
            FaildToLoad = false;

            await RunCommand(() => IsLoading, async () =>
            {
                lastShows = await ApiService.GetLastShowsAsync();
                topShows = await ApiService.GetTopRatedShowsAsync();
            })
            .ContinueWith(t =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (lastShows != null && topShows != null)
                    {
                        // Add items if we had them 
                        foreach (var podcastURL in lastShows)
                        {
                            ModelHelper.AddPodcastViewModel(podcastURL, MostRecentShowsList);
                        }
                        foreach (var podcastURL in topShows)
                        {
                            ModelHelper.AddPodcastViewModel(podcastURL, HighRatedShowList);
                        }
                        DataLoaded?.Invoke(this);

                        return;
                    }

                    // If not set faild flag to true 
                    FaildToLoad = true;
                });
            });
        }

    }
}
