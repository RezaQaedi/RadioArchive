using RadioArchive.Core;
using System;
using System.Linq;
using System.Windows.Media;

namespace RadioArchive
{
    public static class ModelHelper
    {
        /// <summary>
        /// Create's <see cref="PodcastViewModel"/> from a <see cref="PodcastApi"/>
        /// </summary>
        /// <param name="podcast">Podcast api model</param>
        /// <returns></returns>
        public static PodcastViewModel ToPodcastViewModel(this PodcastApi podcast, Color color = default)
        {
            var backgroundColor = color == default ? ColorHelper.GetRandomColor() : color;

            return new PodcastViewModel(podcast.Date, podcast.PodcastTime, backgroundColor, podcast.IsBestOfTheWeek);
        }

        /// <summary>
        /// Create's <see cref="PodcastApi"/> from a <see cref="PodcastViewModel"/>
        /// </summary>
        /// <returns></returns>
        public static PodcastApi ToPodcastUrl(this PodcastViewModel podcastView)
        {
            // Get urls 
            RouteHelper.GetUrlForSpeceficDate(podcastView.Date, podcastView.Time, out var filurl, out var fileUrlReferrer);

            return new PodcastApi(filurl, fileUrlReferrer, podcastView.Date, podcastView.IsReplay, podcastView.Time);
        }

        public static PodcastViewModel ToViewModel(this ShowDataModel dataModel, bool removble = false)
        {
            var view = new PodcastViewModel(dataModel.Date,
                   dataModel.Time,
                   ColorHelper.GetRandomColor(),
                   dataModel.IsReplay, isRemovble:removble);

            if (dataModel.LikeDate != null)
                view.IsLiked = true;

            view.Proggress = dataModel.UserProggresion;

            return view;

        }


        public static ShowDataModel ToDataModel(this PodcastViewModel viewModel, bool isVisited = false)
        {
            var data = new ShowDataModel(viewModel.Date, viewModel.Time)
            {
                IsReplay = viewModel.IsReplay,
                UserProggresion = viewModel.Proggress,
            };

            if (isVisited)
                data.LastVisit = DateTimeOffset.UtcNow;

            return data;
        }

        public static UserNotesDataModel ToDataModel(this PodcastNoteItemViewModel viewModel)        
             => new(viewModel.TextNote, viewModel.Date) { ShowPostion = viewModel.PodcastTime };
        

        public static PodcastNoteItemViewModel ToViewModel(this UserNotesDataModel dataModel)
            => new(dataModel.TextNote, dataModel.Date) { PodcastTime = dataModel.ShowPostion };

        /// <summary>
        /// Adds <see cref="PodcastApi"/> to a <see cref="PodcastListViewModel"/> without duplication
        /// </summary>
        /// <param name="podcastURL"></param>
        /// <param name="viewModels"></param>
        public static void AddPodcastViewModel(PodcastApi podcastURL, PodcastListViewModel viewModels)
        {
            PodcastItemViewModel podcastItemVM = viewModels.Items.FirstOrDefault(p => p.Date == podcastURL.Date);
            var show = podcastURL.ToPodcastViewModel();
            var randomColor = ColorHelper.GetRandomColor();
            var isPlaying = show.Equals(DI.ViewModelPodcastPlayer.CurrnetlyPlayingPodcast);
            var newstWatchedShow = DI.StorgeService.GetVisitedShows().OrderBy(s => s.Date).LastOrDefault();
            var isNew = newstWatchedShow?.Date < show.Date;

            // if we haven't any view model with that date then add it 
            if (podcastItemVM == null)
            {
                // Create new podcast item 
                podcastItemVM = new PodcastItemViewModel(randomColor) 
                {
                    IsNew = isNew,
                };
                podcastItemVM.Shows.Add(show);
                viewModels.Items.Add(podcastItemVM);
            }
            // if we have the same podcast with same date 
            else
            {
                // Check for duplication 
                var itemAlreadyExist = podcastItemVM.Shows.FirstOrDefault(p => p.Date == show.Date && p.Time == show.Time);

                // If we already had this item do nothing 
                if (itemAlreadyExist == null)
                {
                    podcastItemVM.Shows.Add(show);
                }
            }

            // Update is playing 
            if(isPlaying)
                podcastItemVM.IsPlaying = true;
        }
    }
}
