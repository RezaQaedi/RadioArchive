using RadioArchive.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RadioArchive
{
    public class ApplicationStorgeService : IApplicationStorgeService
    {
        #region Private filds 
        /// <summary>
        /// list of shows loaded from db  
        /// </summary>
        private List<ShowDataModel> _shows;

        /// <summary>
        /// list of user created list 
        /// </summary>
        private List<UserCreatedListDataModel> _userPlaylist;

        /// <summary>
        /// List of notes user created 
        /// </summary>
        private List<UserNotesDataModel> _notes;
        #endregion

        /// <summary>
        /// Storge Data base data
        /// </summary>
        public void Load()
        {
            _shows = DI.ClientDataStore.GetShows().ToList();
            _userPlaylist = DI.ClientDataStore.GetAllPlayLists().ToList();
            _notes = DI.ClientDataStore.GetAllNotes().ToList();
        }

        #region Show methods  
        public IEnumerable<ShowDataModel> GetLikedShows()
        {
            return _shows.Where(s => s.LikeDate != null).OrderBy(s => s.LikeDate);
        }

        public IEnumerable<ShowDataModel> GetVisitedShows()
        {
            return _shows.Where(s => s.LastVisit != null).OrderBy(s => s.LastVisit);
        }

        public ShowDataModel GetShow(PodcastViewModel viewModel) => _shows.FirstOrDefault(s => viewModel.Equals(s));

        public void UpdateLike(PodcastViewModel podcastView)
        {
            var match = _shows.FirstOrDefault(s => podcastView.Equals(s));

            if (match == null)
            {
                match = podcastView.ToDataModel();
                _shows.Add(match);
            }

            match.LikeDate = podcastView.IsLiked ? DateTimeOffset.UtcNow : null;
            DI.ClientDataStore.UpdateShow(match);
        }

        public void UpdateVisitDate(PodcastViewModel podcastView)
        {
            var match = _shows.FirstOrDefault(s => podcastView.Equals(s));
            var dataModel = podcastView.ToDataModel();
            dataModel.LastVisit = DateTimeOffset.UtcNow;

            if (match == null)
            {
                match = podcastView.ToDataModel();
                // Add new one 
                _shows.Add(dataModel);
            }

            // Update
            match.LastVisit = DateTimeOffset.UtcNow;
            // Update db 
            DI.ClientDataStore.UpdateShow(dataModel);
        }

        public void UpdateProggress(PodcastViewModel podcastView)
        {
            var data = _shows.FirstOrDefault(s => podcastView.Equals(s));

            if (data != null)
            {
                data.UserProggresion = podcastView.Proggress;
                DI.ClientDataStore.UpdateShow(data);
            }
        }
        #endregion

        #region Play list Methods 
        public IEnumerable<ShowDataModel> GetShows(string Title)
        {
            return _userPlaylist.FirstOrDefault(s => string.Equals(s.Title, Title))?.Shows;
        }

        public void AddShow(PodcastViewModel show, string ListTitle)
        {
            var plaList = _userPlaylist.FirstOrDefault(s => string.Equals(s.Title, ListTitle));

            if (plaList.Shows == null)
                plaList.Shows = new List<ShowDataModel>();

            // TODO : Let user know we already had this item 
            if (plaList.Shows.FirstOrDefault(s => show.Equals(s)) != null)
                return;

            plaList.Shows.Add(show.ToDataModel());
            DI.ClientDataStore.AddToPlayList(show.ToDataModel(), ListTitle);
        }

        public void RemoveShowFromPlayList(PodcastViewModel podcast, string title)
        {
            var matchList = _userPlaylist.FirstOrDefault(l => string.Equals(title, l.Title));

            var matchShow = matchList?.Shows.FirstOrDefault(s => podcast.Equals(s));

            if (matchShow != null)
            {
                DI.ClientDataStore.RemoveFromPlayList(matchShow, matchList);
                matchList.Shows.Remove(matchShow);
            }
        }

        public bool CreateNewList(string title)
        {
            // Check if we had list with same title 
            if (_userPlaylist.FirstOrDefault(l => l.Title == title) != null)
                return false;

            var newList = new UserCreatedListDataModel(DateTimeOffset.UtcNow, title);
            _userPlaylist.Add(newList);

            // update db
            DI.ClientDataStore.CreatePlayList(newList);

            return true;
        }

        public UserCreatedListDataModel GetUserCreatedList(string key) => _userPlaylist.FirstOrDefault(l => l.Title == key);

        public IEnumerable<UserCreatedListDataModel> GetUserList() => _userPlaylist;

        public void RemoveList(string title)
        {
            var list = _userPlaylist.FirstOrDefault(s => string.Equals(s.Title, title));

            if (list != null)
            {
                DI.ClientDataStore.RemovePlaylist(title);
                _userPlaylist.Remove(list);
            }
        }
        #endregion

        #region Note methods 

        public IEnumerable<UserNotesDataModel> GetUserNotes(PodcastViewModel podcastViewModel)
        {
            return _notes.Where(s => podcastViewModel.Equals(s.Show));
        }

        public void AddNotes(PodcastViewModel podcastViewModel, PodcastNoteItemViewModel noteViewModel)
        {
            var NoteData = noteViewModel.ToDataModel();
            NoteData.Show = podcastViewModel.ToDataModel();
            // Update DB
            DI.ClientDataStore.AddNoteToShow(podcastViewModel.ToDataModel(), noteViewModel.ToDataModel());
            // Update storge 
            _notes.Add(NoteData);
        }

        public void RemoveNote(PodcastNoteItemViewModel noteViewModel)
        {
            var match = _notes.Find(n => n.Date == noteViewModel.Date && string.Equals(n.TextNote, noteViewModel.TextNote));

            if (match != null)
            {
                DI.ClientDataStore.RemoveNote(match);
                _notes.Remove(match);
            }
        }

        public void UpdateNote(PodcastNoteItemViewModel noteItemViewModel)
        {
            var match = _notes.FirstOrDefault(n => n.Date == noteItemViewModel.Date);

            // Update chach
            match.TextNote = noteItemViewModel.TextNote;
            DI.ClientDataStore.UpadteNote(match);
        }
        #endregion
    }
}
