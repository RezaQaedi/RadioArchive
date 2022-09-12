using Dna;

namespace RadioArchive
{
    /// <summary>
    /// A shortcut to access class to get DI services 
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// a shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// a shortcut to access the <see cref="PodcastPlayerViewModel"/>
        /// </summary>
        public static PodcastPlayerViewModel ViewModelPodcastPlayer => Framework.Service<PodcastPlayerViewModel>();

        /// <summary>
        /// a shortcut to access the <see cref="IPodcastApiService"/>
        /// </summary>
        public static IPodcastApiService ApiService => Framework.Service<IPodcastApiService>();

    }
}
