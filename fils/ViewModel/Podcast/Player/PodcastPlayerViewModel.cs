using LibVLCSharp.Shared;
using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace RadioArchive
{
    public class PodcastPlayerViewModel : BaseViewModel
    {
        #region Private Filds

        /// <summary>
        /// Media object to control a media 
        /// </summary>
        private readonly MediaPlayer _mediaPlayer;
        private readonly LibVLC _vlc;
        private readonly DispatcherTimer _timer;

        private bool _soundMuted = false;
        private bool _isplaying = false;
        #endregion

        #region Public Properties
        /// <summary>
        /// Medeia that currnetly is playing 
        /// </summary>
        public PodcastURL CurrnetlyPlayingPodcast { get; set; }

        public SpeedRatioMenuViewModel SpeedRatioMenu { get; set; }

        /// <summary>
        /// Title to display 
        /// </summary>
        public string DisplayTitle { get; private set; }

        /// <summary>
        /// Subtitle to display 
        /// </summary>
        public string DisplaySubtitle { get; private set; }

        /// <summary>
        /// Indiactes if we can play media 
        /// </summary>
        public bool CanPlay => CurrnetlyPlayingPodcast != null;

        /// <summary>
        /// Get or set media postion in secound
        /// </summary>
        public float MediaPostion
        {
            get => _mediaPlayer.Position;
            set => _mediaPlayer.Position = value;
        }

        /// <summary>
        /// Media Volume 
        /// </summary>
        public int Volume
        {
            get => _mediaPlayer.Volume;
            set => _mediaPlayer.Volume = value;
        }

        /// <summary>
        /// True if Volume Control should be visible
        /// </summary>
        public bool VolumeControlVisible { get; set; } = false;

        /// <summary>
        /// True if Speed Ratio Menu should be visible
        /// </summary>
        public bool SpeedRatioMenuVisible { get; set; }

        /// <summary>
        /// Indicates if Sound should be muted or not 
        /// </summary>
        public bool SoundMuted
        {
            get => _soundMuted;
            set
            {
                _soundMuted = value;
                _mediaPlayer.Mute = value;
            }
        }

        /// <summary>
        /// Indicate's it media is currently playing retrun false if puased 
        /// </summary>
        public bool IsPlaying
        {
            get { return _isplaying; }
            set
            {
                _isplaying = value;
                if (_isplaying)
                    _timer.Start();
                else
                    _timer.Stop();
            }
        }

        /// <summary>
        /// Time that Media already is in 
        /// </summary>
        public TimeSpan MediaTimeNow { get; private set; }

        /// <summary>
        /// Duration of media 
        /// </summary>
        public TimeSpan MediaDurationTime { get; set; }
        #endregion

        #region Commands
        /// <summary>
        /// Command for Showing voulme 
        /// </summary>
        public ICommand ShowVolumeControlCommand { get; private set; }

        /// <summary>
        /// Command For hiding volume 
        /// </summary>
        public ICommand HideVolumeControlCommand { get; private set; }

        /// <summary>
        /// Command for show or hiding speed Ratio menu 
        /// </summary>
        public ICommand ToggleSpeedRatioMenu { get; set; }

        /// <summary>
        /// Command for play Media 
        /// </summary>
        public ICommand PlayMediaCommand { get; private set; }

        /// <summary>
        /// Command for Pauseing media 
        /// </summary>
        public ICommand PauseMediaCommand { get; set; }

        /// <summary>
        /// Command for toggling sound mute 
        /// </summary>
        public ICommand ToggleMuteCommand { get; set; }

        #endregion

        #region Constructor
        public PodcastPlayerViewModel()
        {
            // Initialize vlclbr
            Core.Initialize();

            _vlc = new LibVLC();
            _mediaPlayer = new MediaPlayer(_vlc);

            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };


            // Creating commands
            PlayMediaCommand = new RelayCommand(PlayMedia);
            PauseMediaCommand = new RelayCommand(PauseMedia);
            ShowVolumeControlCommand = new RelayCommand(() => VolumeControlVisible = true);
            HideVolumeControlCommand = new RelayCommand(() => VolumeControlVisible = false);
            ToggleMuteCommand = new RelayCommand(() => SoundMuted = !SoundMuted);
            ToggleSpeedRatioMenu = new RelayCommand(() => SpeedRatioMenuVisible = !SpeedRatioMenuVisible);

            // Vlc events 
            _mediaPlayer.TimeChanged += TimeChanged;
            //_mediaPlayer.PositionChanged += PositionChanged;
            _mediaPlayer.LengthChanged += LengthChanged;
            _mediaPlayer.EndReached += EndReached;
            _mediaPlayer.Playing += Playing;
            _mediaPlayer.Paused += Paused;

            // Timer event's
            _timer.Tick += TimerTick;

            // SpeedMenu Ratio
            SpeedRatioMenu = new SpeedRatioMenuViewModel();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(MediaPostion));
        }

        private void Playing(object sender, EventArgs e)
        {
            IsPlaying = true;
        }

        private void Paused(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        private void EndReached(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        private void LengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            MediaDurationTime = TimeSpan.FromMilliseconds(_mediaPlayer.Length);
        }

        private void TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            MediaTimeNow = TimeSpan.FromMilliseconds(_mediaPlayer.Time);
        }

        #endregion

        #region Private Methods 



        #endregion

        #region Public methods 
        /// <summary>
        /// Play Media 
        /// </summary>
        /// <remarks>
        /// </remarks>
        public void PlayMedia()
        {
            if (CanPlay)
            {
                _mediaPlayer.Play();
            }
        }

        public void PauseMedia()
        {
            _mediaPlayer.Pause();
        }

        public void ChangeRatio(float rate)
        {
            _mediaPlayer.SetRate(rate);
        }

        /// <summary>
        /// Open spesefic podcast 
        /// </summary>
        /// <param name="podcast">Podcast wanted to load in media player</param>
        public void OpenPodcast(PodcastURL podcast, bool autoPlay = true)
        {
            CurrnetlyPlayingPodcast = podcast;
            DisplayTitle = podcast.PodcastTime == PodcastTime.Evening ? "Evening" : "Morning";
            DisplayTitle += podcast.IsBestOfTheWeek ? " (Best of the week)" : "";
            DisplaySubtitle = podcast.Date.ToString("ddd  yyy/MM/dd");

            // Feed data to vlc 
            using (var media = new Media(_vlc, new Uri(CurrnetlyPlayingPodcast.Url)))
            {
                media.AddOption($":http-referrer={CurrnetlyPlayingPodcast.UrlReferer}");
                _mediaPlayer.Media = media;
            };

            // Play the media 
            if (autoPlay)
                PlayMedia();
        }
        #endregion
    }
}
