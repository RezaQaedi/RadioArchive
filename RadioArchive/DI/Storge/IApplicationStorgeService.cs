using RadioArchive.Core;
using System.Collections.Generic;

namespace RadioArchive
{
    public interface IApplicationStorgeService
    {
        /// <summary>
        /// Storge Data base 
        /// </summary>
        void Load();

        IEnumerable<ShowDataModel> GetLikedShows();
        ShowDataModel GetShow(PodcastViewModel viewModel);
        IEnumerable<ShowDataModel> GetVisitedShows();
        UserCreatedListDataModel GetUserCreatedList(string key);

        IEnumerable<UserCreatedListDataModel> GetUserList();

        /// <summary>
        /// Gets list of shows in specefic list 
        /// </summary>
        /// <param name="Title">List title to get from</param>
        /// <returns></returns>
        IEnumerable<ShowDataModel> GetShows(string Title);

        void UpdateLike(PodcastViewModel podcastView);
        void UpdateVisitDate(PodcastViewModel podcastView);
        void UpdateProggress(PodcastViewModel podcastView);

        /// <summary>
        /// Adds a show to speceifc list 
        /// </summary>
        /// <param name="show">Show to add</param>
        /// <param name="ListTitle">List title to add to</param>
        void AddShow(PodcastViewModel show, string ListTitle);

        /// <summary>
        /// Creates new user show list 
        /// </summary>
        /// <param name="title">Title of show (Must be uinqe)</param>
        bool CreateNewList(string title);

        /// <summary>
        /// Remove user list 
        /// </summary>
        /// <param name="title">title of list wanted to be removed</param>
        /// <returns></returns>
        void RemoveList(string title);

        /// <summary>
        /// Get notes of specefic <see cref="PodcastViewModel"/>
        /// </summary>
        /// <param name="podcastViewModel"></param>
        /// <returns></returns>
        IEnumerable<UserNotesDataModel> GetUserNotes(PodcastViewModel podcastViewModel);

        /// <summary>
        /// Add new note to <paramref name="podcastViewModel"/>
        /// </summary>
        /// <param name="podcastViewModel"></param>
        /// <param name="noteViewModel"></param>
        /// <returns></returns>
        void AddNotes(PodcastViewModel podcastViewModel, PodcastNoteItemViewModel noteViewModel);

        /// <summary>
        /// Removes note 
        /// </summary>
        /// <param name="noteViewModel">Note to remove</param>
        /// <returns></returns>
        void RemoveNote(PodcastNoteItemViewModel noteViewModel);

        /// <summary>
        /// Updates specefic note 
        /// </summary>
        /// <param name="noteItemViewModel"></param>
        /// <returns></returns>
        void UpdateNote(PodcastNoteItemViewModel noteItemViewModel);

        /// <summary>
        /// Remove a show from playlist 
        /// </summary>
        /// <param name="podcast">Show to remove</param>
        /// <param name="title">Play list to remove from</param>
        /// <returns></returns>
        void RemoveShowFromPlayList(PodcastViewModel podcast, string title);
    }
}
