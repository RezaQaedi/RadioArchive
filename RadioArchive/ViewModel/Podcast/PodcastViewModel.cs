using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using static RadioArchive.DI;
using RadioArchive.Core;
using System.Windows;
using System.Threading.Tasks;

namespace RadioArchive
{
    public class PodcastViewModel : BaseViewModel
    {
        #region Private methods
        private bool _moreMenuOpen;
        private readonly WpfMenuItemViewModel _playListMenu;
        private readonly bool _isRemoveble; 
        #endregion

        #region Public properties

        /// <summary>
        /// Title to show 
        /// </summary>
        public string DisplayTitle => Date.ToString("dddd");

        /// <summary>
        /// Get or set podcast time 
        /// </summary>
        public PodcastTime Time { get; set; }

        /// <summary>
        /// List of note this item have 
        /// </summary>
        public PodcastNoteListViewModel Notes { get; set; }

        /// <summary>
        /// Indicate's this podcast is replay show 
        /// </summary>
        public bool IsReplay { get; set; }

        /// <summary>
        /// Get's if this podcast dosnt have any specific date 
        /// </summary>
        public bool NoSpecificDate => Time == PodcastTime.None;

        /// <summary>
        /// Inidcates if this podcast is liked before 
        /// </summary>
        public bool IsLiked { get; set; }

        /// <summary>
        /// Indicates if this podcast is playing now 
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Indicates if note shoud be visible 
        /// </summary>
        public bool NotesVisible { get; set; }

        /// <summary>
        /// Proggress of watching this item between  0 and 100 
        /// </summary>
        public float Proggress { get; set; }

        /// <summary>
        /// User written input 
        /// </summary>
        public string NotesInput { get; set; }

        /// <summary>
        /// Color of background 
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        ///  50% Lighter version of <see cref="BackgroundColor"/>
        /// </summary>
        public Color BackgroundColorLighter => BackgroundColor.Lerp(Colors.White, 0.5f);

        /// <summary>
        /// The date of relase 
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Flag for updaing on more menu state 
        /// </summary>
        public bool MoreMenuOpen
        {
            get => _moreMenuOpen;
            set
            {
                //UpdateUserCreatedListMenu();
                _moreMenuOpen = value;
            }
        }

        public ObservableCollection<WpfMenuItemViewModel> UserCreatedPlatlistMenu { get; set; }
        #endregion

        #region Commands

        /// <summary>
        /// Command for playing or pausing media 
        /// </summary>
        public ICommand TogglePlayCommand { get; set; }

        /// <summary>
        /// Command for show or hiding the notes 
        /// </summary>
        public ICommand ToggleNotes { get; set; }

        /// <summary>
        /// Command for adding this item to liked list 
        /// </summary>
        public ICommand LikeCommand { get; set; }

        /// <summary>
        /// Command for saveing user input to notes lits 
        /// </summary>
        public ICommand SaveInputCommand { get; set; }
        #endregion

        #region Counstractor
        public PodcastViewModel(DateTimeOffset date, PodcastTime podcastTime, Color color, bool isReplay = false, bool isRemovble = false)
        {
            Time = podcastTime;
            Date = date;
            IsReplay = isReplay;
            BackgroundColor = color;
            _isRemoveble = isRemovble;

            // Create notes 
            Notes = new PodcastNoteListViewModel
            {
                Items = new ObservableCollection<PodcastNoteItemViewModel>()
            };
            // Get all of podcast notes 
            var notesDataModels = StorgeService.GetUserNotes(this);
            if (notesDataModels != null)
                foreach (var noteData in notesDataModels)
                {
                    var viewModel = noteData.ToViewModel();
                    viewModel.SeekToTimeCommand =
                        new RelayCommand(() => ViewModelPodcastPlayer.SeekTo(viewModel.PodcastTime, this));
                    viewModel.RemoveAction = RemoveNote;
                    Notes.Items.Add(viewModel);
                }

            // Create menus 
            UserCreatedPlatlistMenu = new ObservableCollection<WpfMenuItemViewModel>();
            _playListMenu = new WpfMenuItemViewModel()
            {
                Header = "Add to playlist",
                Icon = IconType.RightSide
            };
            var mainMenu = new WpfMenuItemViewModel()
            {
                Header = "",
                Icon = IconType.MoreVertical,
                MenuItems = new ObservableCollection<WpfMenuItemViewModel>()
                {
                        _playListMenu,
                        new WpfMenuItemViewModel(){Header = "Share", Icon = IconType.Share, Command = new RelayCommand(CopyPodcastPage)},
                        // TODO : Add download option
                },
            };

            // If this podcast is removble from list 
            if (_isRemoveble)
                mainMenu.MenuItems.Add(new WpfMenuItemViewModel()
                {
                    Header = "Remove",
                    Icon = IconType.Remove,
                    Command =
                    new RelayCommand(() => ViewModelApplication.RemovePodcast(this))
                });

            UserCreatedPlatlistMenu.Add(mainMenu);

            // Commands 
            TogglePlayCommand = new RelayCommand(() =>
            {
                ViewModelPodcastPlayer.OpenPodcast(this);
            });

            LikeCommand = new RelayCommand(ChangeLike);

            ToggleNotes = new RelayCommand(() => NotesVisible = !NotesVisible);
            SaveInputCommand = new RelayCommand(() => SaveNoteInput());

            // Menu Events
            mainMenu.SubMenuOpend += MainMenuOpend;
        }

        #endregion

        #region Public Mthods 


        public async void RemoveNote(PodcastNoteItemViewModel noteItemViewModel)
        {
            await RunCommand(() => Working, async () =>
            {
                await Task.Run(() => StorgeService.RemoveNote(noteItemViewModel));
            });

            Notes.Items.Remove(noteItemViewModel);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is PodcastViewModel vm)
                return Date == vm.Date && Time == vm.Time;
            if (obj is ShowDataModel dataModel)
                return Date == dataModel.Date && Time == dataModel.Time;

            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Date, Time);
        }
        #endregion

        #region Private methods 

        /// <summary>
        /// Updates this podcast like status 
        /// </summary>
        private async void ChangeLike()
        {

            IsLiked = !IsLiked;

            await RunCommand(() => Working, async () =>
            {
                await Task.Run(() => StorgeService.UpdateLike(this));
            });
        }

        /// <summary>
        /// Copy link of this podcast page to clipboard 
        /// </summary>
        private void CopyPodcastPage()
        {
            var pageUrl = RouteHelper.GetShowPage(Date, Time);
            Clipboard.SetText(pageUrl);
            // TODO : Let user know link has been copied into clipboard 
        }

        /// <summary>
        /// Triggers when main menu opend 
        /// </summary>
        /// <param name="menu"></param>
        private void MainMenuOpend(WpfMenuItemViewModel menu)
        {
            _playListMenu.MenuItems = new ObservableCollection<WpfMenuItemViewModel>();
            foreach (var item in StorgeService.GetUserList())
            {
                _playListMenu.MenuItems.Add(new WpfMenuItemViewModel() { Header = item.Title, Command = new RelayCommand(() => AddThisToUsePlayList(item.Title)) });
            }
        }

        /// <summary>
        /// Saves notes eneterd by user to notes list 
        /// </summary>
        private async void SaveNoteInput()
        {
            if (string.IsNullOrEmpty(NotesInput))
                return;

            var viewModel = new PodcastNoteItemViewModel(NotesInput.Trim(),
                DateTimeOffset.UtcNow);
            viewModel.SeekToTimeCommand =
                    new RelayCommand(() => ViewModelPodcastPlayer.SeekTo(viewModel.PodcastTime, this));
            viewModel.RemoveAction = RemoveNote;

            if (Equals(ViewModelPodcastPlayer.CurrnetlyPlayingPodcast))
                viewModel.PodcastTime = ViewModelPodcastPlayer.MediaTimeNow;

            await RunCommand(() => Working, async () =>
            {
                await Task.Run(() => StorgeService.AddNotes(this, viewModel));
            });


            Notes.Items.Insert(0, viewModel);

            NotesInput = "";
        }

        /// <summary>
        /// Add this podcast to specefic user play list 
        /// </summary>
        /// <param name="listKey">key to specify user play list</param>
        private async void AddThisToUsePlayList(string listKey)
        {
            await RunCommand(() => Working, async () =>
            {
                await Task.Run(() => StorgeService.AddShow(this, listKey));
            });
        } 
        #endregion 
    }
}
