using static RadioArchive.DI;

namespace RadioArchive
{
    /// <summary>
    /// locates view models from IoC for use in XAML files
    /// </summary>
    public class ViewModelLocator
    {
        #region Public properties
        /// <summary>
        /// singleton instance of the locater
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// the app view model
        /// </summary>
        public ApplicationViewModel ApplicationViewModel => ViewModelApplication;

        /// <summary>
        /// App main media player 
        /// </summary>
        public PodcastPlayerViewModel PodcastPlayerViewModel => ViewModelPodcastPlayer;

        #endregion
    }
}
