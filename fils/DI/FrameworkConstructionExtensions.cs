using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace RadioArchive
{
    /// <summary>
    /// Extensions method for <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view model needed for Word Application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddViewModels(this FrameworkConstruction construction)
        {
            // Bind singleton instance of application view model
            construction.Services.AddSingleton<ApplicationViewModel>();
            construction.Services.AddSingleton<PodcastPlayerViewModel>();
            // Return the constructions for chaining 
            return construction;
        }
        
        /// <summary>
        /// Injects the Word client application needed services needed for word application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddWordClientServices(this FrameworkConstruction construction)
        {
            //TODO 
            // Add the task manager 
            //construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            // Bind a File Manager
            //construction.Services.AddTransient<IFileManager, BaseFileManager>();

            // Bind an UI Manager
            //construction.Services.AddTransient<IUIManager, UIManger>();

            // Add API helper
            construction.Services.AddSingleton<IPodcastApiService, PodcastApiService>();

            // Return the constructions for chaining 
            return construction;
        }
    }
}
