using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadioArchive
{
    public class PlaylistViewModel : BaseViewModel
    {
        /// <summary>
        /// Title of this play list 
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Indicates if there are no item here 
        /// </summary>
        public bool EmptyList => Items.Count == 0;

        /// <summary>
        /// Command for closing 
        /// </summary>
        public ICommand BackCommand { get; set; }


        /// <summary>
        /// List of shows 
        /// </summary>
        public ObservableCollection<PodcastViewModel> Items { get; set; }

        public PlaylistViewModel()
        {
            Items = new ObservableCollection<PodcastViewModel>();

            BackCommand = new RelayCommand(() =>
            {
                DI.ViewModelApplication.PlayListVisible = false;
            });

            // Events 
            DI.ViewModelPodcastPlayer.StartPlaying += OnPodcastStarts;
            DI.ViewModelPodcastPlayer.PodcastEndReached += OnPodcastEndReaced;
            DI.ViewModelPodcastPlayer.PodcastPaused += OnPodcastEndReaced;
            DI.ViewModelPodcastPlayer.PositonChanged += OnPositonChanged; 
        }

        private void OnPositonChanged(PodcastViewModel currentlyPlaying)
        {
            var podcast = Items?.FirstOrDefault(p => p.Equals(currentlyPlaying));

            if(podcast != null)
                podcast.Proggress = currentlyPlaying.Proggress;
        }

        private void OnPodcastEndReaced(PodcastViewModel currentlyPlaying)
        {
            var podcast = Items?.FirstOrDefault(p => p.Equals(currentlyPlaying));

            if (podcast != null)
                podcast.IsPlaying = currentlyPlaying.IsPlaying;
        }

        private void OnPodcastStarts(PodcastViewModel currentlyPlaying)
        {
            var podcast = Items?.FirstOrDefault(p => p.Equals(currentlyPlaying));

            if (podcast != null)
                podcast.IsPlaying = currentlyPlaying.IsPlaying;
        }

        public override void Dispose()
        {
            DI.ViewModelPodcastPlayer.StartPlaying -= OnPodcastStarts;
            DI.ViewModelPodcastPlayer.PodcastEndReached -= OnPodcastEndReaced;
            DI.ViewModelPodcastPlayer.PodcastPaused -= OnPodcastEndReaced;
            DI.ViewModelPodcastPlayer.PositonChanged -= OnPositonChanged;

            base.Dispose(); 
        }

        public void AddItems(IEnumerable<PodcastViewModel> viewModels, bool isRemovble)
        {
            // Add new items 
            Items.Clear();            
            foreach (var show in viewModels)
            {

                var localData = DI.StorgeService.GetShow(show);

                PodcastViewModel podcast;

                if (localData != null)
                    podcast = localData.ToViewModel(removble:isRemovble);
                else
                    podcast = new PodcastViewModel(show.Date, show.Time,
                        ColorHelper.GetRandomColor(), show.IsReplay, isRemovble:isRemovble);

                // Update Playing flag 
                if (DI.ViewModelPodcastPlayer.IsPlaying && podcast.Equals(DI.ViewModelPodcastPlayer.CurrnetlyPlayingPodcast))
                    podcast.IsPlaying = true;                 

                Items.Insert(0, podcast);
            }

            // Let others know of changes  
            OnPropertyChanged(nameof(EmptyList));
        }
    }
}
