using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RadioArchive
{
    public class PodcastNoteItemViewModel : BaseViewModel
    {
        /// <summary>
        /// Text of this note 
        /// </summary>
        public string TextNote { get; set; }

        /// <summary>
        /// non-comited text 
        /// </summary>
        public string EditedText { get; set; }

        /// <summary>
        /// Indicates if the current text is in edit mode 
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// Action when removing this note 
        /// </summary>
        public Action<PodcastNoteItemViewModel> RemoveAction { get; set; }

        /// <summary>
        /// Time of this note has been written
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Time span of media time when this note has been written 
        /// </summary>
        public TimeSpan PodcastTime { get; set; }

        /// <summary>
        /// Command for seeking into this note time 
        /// </summary>
        public ICommand SeekToTimeCommand { get; set; }

        /// <summary>
        /// Command for Canceling editing 
        /// </summary>
        public ICommand CancelEditCommand { get; set; }

        /// <summary>
        /// Command for saveing edited text 
        /// </summary>
        public ICommand SaveEditCommand { get; set; }

        /// <summary>
        /// Collection of menu items for option menu 
        /// </summary>
        public ObservableCollection<WpfMenuItemViewModel> Options { get; set; }

        public PodcastNoteItemViewModel(string textNote, DateTimeOffset date, TimeSpan podcastTime = default)
        {
            TextNote = textNote;
            Date = date;
            PodcastTime = podcastTime;

            // Create commands 
            CancelEditCommand = new RelayCommand(() => Editing = false);
            SaveEditCommand = new RelayCommand(Save);

            // Create menus 
            Options = new ObservableCollection<WpfMenuItemViewModel>();
            var mainMenu = new WpfMenuItemViewModel()
            {
                Header = "",
                Icon = IconType.MoreVertical,
                MenuItems = new ObservableCollection<WpfMenuItemViewModel>()
                {
                    new WpfMenuItemViewModel(){Header = "Copy", Icon = IconType.Copy, Command = new RelayCommand(() => Clipboard.SetText(TextNote))},
                    new WpfMenuItemViewModel(){Header = "Edit", Icon = IconType.Edit, Command = new RelayCommand(Edit)},
                    new WpfMenuItemViewModel(){Header = "Remove", Icon = IconType.Remove, Command = new RelayCommand(Remove)},
                },
            };
            Options.Add(mainMenu);
        }

        /// <summary>
        /// Commits edits and save the value 
        /// </summary>
        private async void Save()
        {
            Editing = false;
            TextNote = EditedText.Trim();

            await RunCommand(() => Working, async () => 
            {
                await Task.Run(() => DI.StorgeService.UpdateNote(this));
            });
        }

        /// <summary>
        /// Puts the current control in edit mod 
        /// </summary>
        private void Edit()
        {
            EditedText = TextNote;
            Editing = true;
        }

        /// <summary>
        /// will remove this item from Db 
        /// </summary>
        private void Remove()
        {
            RemoveAction?.Invoke(this);
        }
    }
}
