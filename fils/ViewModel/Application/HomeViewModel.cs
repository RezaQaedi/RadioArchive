using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using static RadioArchive.DI;

namespace RadioArchive
{
    public class HomeViewModel : BaseViewModel
    {
        public PodcastListViewModel MostRecentShowsList { get; set; }
        public PodcastListViewModel HighRatedShowList { get; set; }

        public bool ListIsLoading { get; set; }

        private readonly Random _random;

        public HomeViewModel()
        {
            _random = new Random();

            // Creat view mdoels
            MostRecentShowsList = new PodcastListViewModel() { DisplayTitle = "Most recent shows", HasMoreContent = true };
            HighRatedShowList = new PodcastListViewModel() { DisplayTitle = "Top rated shows" };

#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

#endif

            LoadData();

        }


        public async void LoadData()
        {
            await RunCommand(() => ListIsLoading, async () =>
            {
                var data = await ApiService.GetLastShowsAsync();

                foreach (var podcastURL in data)
                {
                    AddPodcastViewModel(podcastURL, MostRecentShowsList);
                }

                var topratedShows = await ApiService.GetTopRatedShowsAsync();

                foreach (var podcastURL in topratedShows)
                {
                    AddPodcastViewModel(podcastURL, HighRatedShowList);
                }
            });
        }

        public void AddPodcastViewModel(PodcastURL podcastURL, PodcastListViewModel viewModels)
        {
            PodcastItemViewModel podcastVM = viewModels.Items.FirstOrDefault(p => p.Date == podcastURL.Date);
            var show = podcastURL.ToPodcastViewModel();
            var randomColor = Color.FromRgb((byte)_random.Next(225), (byte)_random.Next(225), (byte)_random.Next(225));

            // if we haven't any view model with that date then add it 
            if (podcastVM == null)
            {
                viewModels.Items.Add(podcastURL.ToPodcastItemViewModel(randomColor));
            }
            // if we have the same podcast with same date 
            else
            {
                // Add new show based on time 
                if (show.Time == PodcastTime.Morning || show.Time == PodcastTime.none)
                    podcastVM.MorningShow = show;
                else if (show.Time == PodcastTime.Evening)
                    podcastVM.EveningShow = show;
            }
        }

    }
}
