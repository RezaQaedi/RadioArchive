using LibVLCSharp.Shared;
using RadioArchive.Core;
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
        private readonly MediaPlayer _recorder;

        private bool _soundMuted = false;
        private bool _isplaying = false;
        #endregion

        #region Public Properties
        /// <summary>
        /// Medeia that currnetly is playing 
        /// </summary>
        public PodcastViewModel CurrnetlyPlayingPodcast { get; set; }

        /// <summary>
        /// Menu view model for ratio 
        /// </summary>
        public SpeedRatioMenuViewModel SpeedRatioMenu { get; set; }

        /// <summary>
        /// Triggers when media start playing 
        /// </summary>
        public event Action<PodcastViewModel> StartPlaying;

        /// <summary>
        /// Triggers when media stopped playing 
        /// </summary>
        public event Action<PodcastViewModel> PodcastEndReached;

        /// <summary>
        /// Triggers when new media opend 
        /// </summary>
        public event Action<PodcastViewModel> PodcastOpend;

        /// <summary>
        /// Triggers when playing postion chanaged 
        /// </summary>
        public event Action<PodcastViewModel> PositonChanged;

        /// <summary>
        /// Triggers when media paused 
        /// </summary>
        public event Action<PodcastViewModel> PodcastPaused;

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
        /// Indicate's it media is currently playing
        /// </summary>
        public bool IsPlaying
        {
            get => _isplaying;
            set
            {
                _isplaying = value;
                CurrnetlyPlayingPodcast.IsPlaying = value;
                if (value)
                    _timer.Start();
                else
                    _timer.Stop();
            }
        }

        /// <summary>
        /// Indicates if recorder is recording 
        /// </summary>
        public bool IsRecording { get; set; }

        /// <summary>
        /// Indicates if recorder is reaching to the end postion 
        /// </summary>
        public bool IsRecorderBusy { get; set; }

        /// <summary>
        /// start float of record media postion 
        /// </summary>
        public float RecorderStartPostion { get; set; }

        /// <summary>
        /// End float of record postion 
        /// </summary>
        public float RecorderEndPostion { get; set; }

        /// <summary>
        /// Time that Media already is in 
        /// </summary>
        public TimeSpan MediaTimeNow { get; private set; }

        /// <summary>
        /// Duration of media 
        /// </summary>
        public TimeSpan MediaDurationTime { get; private set; }
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

        /// <summary>
        /// Command for skiping 15 seconds 
        /// </summary>
        public ICommand SeekNextCommand { get; set; }

        /// <summary>
        /// Command for skiping back 15 seconds 
        /// </summary>
        public ICommand SeekPervCommand { get; set; }

        /// <summary>
        /// Command for start recording 
        /// </summary>
        public ICommand StartRecordCommand { get; set; }

        /// <summary>
        /// Command for stop recording 
        /// </summary>
        public ICommand StopRecordCommand { get; set; }

        #endregion

        #region Constructor
        public PodcastPlayerViewModel()
        {
            // Initialize vlclbr
            LibVLCSharp.Shared.Core.Initialize();

            _vlc = new LibVLC();
            _mediaPlayer = new MediaPlayer(_vlc);
            _recorder = new(_vlc);
            _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };


            // Creating commands
            PlayMediaCommand = new RelayCommand(PlayMedia);
            PauseMediaCommand = new RelayCommand(PauseMedia);
            SeekNextCommand = new RelayCommand(SeekNext);
            SeekPervCommand = new RelayCommand(SeekPerv);
            ShowVolumeControlCommand = new RelayCommand(() => VolumeControlVisible = true);
            HideVolumeControlCommand = new RelayCommand(() => VolumeControlVisible = false);
            ToggleMuteCommand = new RelayCommand(() => SoundMuted = !SoundMuted);
            ToggleSpeedRatioMenu = new RelayCommand(() => SpeedRatioMenuVisible = !SpeedRatioMenuVisible);
            StartRecordCommand = new RelayCommand(StartRecorder);
            StopRecordCommand = new RelayCommand(StopRecorder);

            // Vlc events 
            _mediaPlayer.TimeChanged += OnMediaTimeChanged;
            _mediaPlayer.LengthChanged += OnMediaLengthChanged;
            _mediaPlayer.EndReached += OnMediaEndReached;
            _mediaPlayer.Playing += OnMediaPlaying;
            _mediaPlayer.Paused += OnMediaPaused;

            // Recorder 
            _recorder.PositionChanged += OnRecorderPostinChanged;
            _recorder.Playing += OnRecorderPlaying;
            _recorder.Paused += OnRecorderPaused;
            _recorder.Stopped += OnRecorderStopped;

            // Timer events
            _timer.Tick += TimerTick;

            // SpeedMenu Ratio
            SpeedRatioMenu = new SpeedRatioMenuViewModel();
        }

        #endregion

        #region Private Methods 

        #region Recoder Methods

        /// <summary>
        /// Satarts recording of currnet playing media 
        /// </summary>
        private void StartRecorder()
        {
            if (_recorder.State == VLCState.Stopped || _recorder.State == VLCState.Error)
                OpenRecorder(CurrnetlyPlayingPodcast);

            IsRecording = _recorder.Play();
            _recorder.Position = _mediaPlayer.Position;
            RecorderStartPostion = _mediaPlayer.Position;
        }

        /// <summary>
        /// Opens and records given podcast 
        /// </summary>
        /// <param name="podcast"></param>
        private void OpenRecorder(PodcastViewModel podcast)
        {
            using var media = _vlc.GetNewMedia(podcast.Date, podcast.Time, VLCMediaOptions.Referrer, 
                VLCMediaOptions.File, VLCMediaOptions.Keep);
            _recorder.Media = media;
        }

        /// <summary>
        /// Stops recording
        /// </summary>
        private void StopRecorder()
        {
            _recorder.Stop();
            IsRecording = false;
        }

        /// <summary>
        /// Triggers when recorder postion changed 
        /// </summary>
        private void OnRecorderPostinChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {

            // Check if recorder reach the player 
            if (_recorder.Position >= _mediaPlayer.Position)
            {
                // Pause it 
                if (_recorder.IsPlaying)
                {
                    RecorderEndPostion = _recorder.Position;
                    // Could make deadlock ?
                    _recorder.Pause();
                }
            }
        }

        private void OnRecorderStopped(object sender, EventArgs e)
        {
            IsRecorderBusy = false;
        }

        private void OnRecorderPaused(object sender, EventArgs e)
        {
            IsRecorderBusy = false;
        }

        private void OnRecorderPlaying(object sender, EventArgs e)
        {
            IsRecorderBusy = true;
        }

        #endregion

        #region Mediaplayer methods 
        private void TimerTick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(MediaPostion));
            CurrnetlyPlayingPodcast.Proggress = _mediaPlayer.Position * 100;
            PositonChanged?.Invoke(CurrnetlyPlayingPodcast);

            // Check if recorder done saveing past data start it again 
            if (IsRecording)
            {
                if (_mediaPlayer.Position > _recorder.Position)
                {
                    if (_recorder.IsPlaying == false)  
                        _recorder.Play();
                }
            }
        }

        private void OnMediaPlaying(object sender, EventArgs e)
        {
            IsPlaying = true;
            CurrnetlyPlayingPodcast.IsPlaying = true;
            StartPlaying?.Invoke(CurrnetlyPlayingPodcast);
        }

        private void OnMediaPaused(object sender, EventArgs e)
        {
            IsPlaying = false;
            SaveChanges();
            PodcastPaused?.Invoke(CurrnetlyPlayingPodcast);
        }

        private void OnMediaEndReached(object sender, EventArgs e)
        {
            IsPlaying = false;
            PodcastEndReached?.Invoke(CurrnetlyPlayingPodcast);
        }

        private void OnMediaLengthChanged(object sender, MediaPlayerLengthChangedEventArgs e)
        {
            MediaDurationTime = TimeSpan.FromMilliseconds(_mediaPlayer.Length);
        }

        private void OnMediaTimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            MediaTimeNow = TimeSpan.FromMilliseconds(_mediaPlayer.Time);
        }
        #endregion

        #region Updating methods 
        /// <summary>
        ///  Saves data changes on Currently playing to Storge 
        /// </summary>
        private void SaveChanges()
        {
            if (CurrnetlyPlayingPodcast != null)
                DI.StorgeService.UpdateProggress(CurrnetlyPlayingPodcast);
        }

        /// <summary>
        /// Updates details with given <see cref="PodcastViewModel"/>
        /// </summary>
        /// <param name="podcast"></param>
        private void UpdateMeidaDetails(PodcastViewModel podcast)
        {
            DisplayTitle = podcast.Time == PodcastTime.None ? "" : podcast.Time.ToString();
            DisplayTitle += podcast.IsReplay ? " (Best of the week)" : "";
            DisplaySubtitle = podcast.Date.ToString("ddd  yyy/MM/dd");
        }

        #endregion

        #endregion

        #region Public methods 

        #region Media control 
        /// <summary>
        /// Play Media 
        /// </summary>
        public void PlayMedia()
        {
            if (CanPlay)
            {
                _mediaPlayer.Play();
            }
        }

        /// <summary>
        /// Pause media 
        /// </summary>
        public void PauseMedia()
        {
            _mediaPlayer.Pause();
        }

        /// <summary>
        /// Change meida speed play back 
        /// </summary>
        /// <param name="rate"></param>
        public void ChangeRatio(float rate)
        {
            _mediaPlayer.SetRate(rate);
        }

        /// <summary>
        /// Seek to specefic time span  
        /// </summary>
        /// <param name="timeSpan">Time to seek to</param>
        /// <param name="podcast">Podcast to play from</param>
        public void SeekTo(TimeSpan timeSpan, PodcastViewModel podcast)
        {
            // if this is new item first open it 
            if (!podcast.Equals(CurrnetlyPlayingPodcast))
                OpenPodcast(podcast, startFromLastVisit: false);

            _mediaPlayer.SeekTo(timeSpan);

            if (!IsPlaying)
                PlayMedia();
        }

        public void SeekNext() => _mediaPlayer.SeekTo(MediaTimeNow + TimeSpan.FromSeconds(15));
        public void SeekPerv() => _mediaPlayer.SeekTo(MediaTimeNow - TimeSpan.FromSeconds(15));
        #endregion

        /// <summary>
        /// Open spesefic podcast 
        /// </summary>
        /// <param name="podcast">Podcast wanted to load in media player</param>
        public void OpenPodcast(PodcastViewModel podcast, bool autoPlay = true, bool startFromLastVisit = true)
        {
            // if called with same podcastViewmodel just toggle playing  
            if (podcast.Equals(CurrnetlyPlayingPodcast))
            {
                if (IsPlaying)
                    PauseMedia();
                else
                    PlayMedia();

                return;
            }

            // Update old podcast 
            if (CurrnetlyPlayingPodcast != null)
                CurrnetlyPlayingPodcast.IsPlaying = false;
            SaveChanges();

            // Add new one 
            CurrnetlyPlayingPodcast = podcast;
            UpdateMeidaDetails(podcast);

            // Create media 
            using var media = _vlc.GetNewMedia(podcast.Date, podcast.Time, VLCMediaOptions.Referrer);
            _mediaPlayer.Media = media;

            // stop recorder if its in recording state 
            if(IsRecording)
                StopRecorder();
            // Set up new podcast for recorder 
            OpenRecorder(podcast);

            // Play the media 
            if (autoPlay)
                PlayMedia();

            if (startFromLastVisit)
                // Set user proggress 
                if (CurrnetlyPlayingPodcast.Proggress > 0)
                    _mediaPlayer.Position = CurrnetlyPlayingPodcast.Proggress / 100f;

            PodcastOpend?.Invoke(CurrnetlyPlayingPodcast);
        }

        #endregion
    }
}
