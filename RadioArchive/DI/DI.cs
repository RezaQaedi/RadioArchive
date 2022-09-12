using Dna;
using RadioArchive.Core;

namespace RadioArchive
{
    /// <summary>
    /// A shortcut to access class to get DI services 
    /// </summary>
    public class DI
    {
        /// <summary>
        /// a shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => Framework.Service<IUIManager>();

        /// <summary>
        /// a shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// a shortcut to access the <see cref="PodcastPlayerViewModel"/>
        /// </summary>
        public static PodcastPlayerViewModel ViewModelPodcastPlayer => Framework.Service<PodcastPlayerViewModel>();

        /// <summary>
        /// a shortcut to access the <see cref="IClientDataStore"/> service
        /// </summary>
        public static IClientDataStore ClientDataStore => Framework.Service<IClientDataStore>();

        /// <summary>
        /// a shortcut to access the <see cref="IPodcastApiService"/>
        /// </summary>
        public static IPodcastApiService ApiService => Framework.Service<IPodcastApiService>();

        /// <summary>
        /// a shortcut to access the <see cref="IApplicationStorgeService"/>
        /// </summary>
        public static IApplicationStorgeService StorgeService => Framework.Service<IApplicationStorgeService>();

    }
}
