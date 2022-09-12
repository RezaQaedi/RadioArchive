using System.Collections.ObjectModel;

namespace RadioArchive
{
    public class PodcastNoteListViewModel : BaseViewModel
    {
        public ObservableCollection<PodcastNoteItemViewModel> Items { get; set; }

        /// <summary>
        /// Indicates if there is any note in this podcast or not 
        /// </summary>
        public bool NoteEmpty => Items.Count == 0;
    }
}
