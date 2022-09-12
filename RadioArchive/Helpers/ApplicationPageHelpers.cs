using System.Diagnostics;

namespace RadioArchive
{
    /// <summary>
    /// converts the <see cref="ApplicationPage"/> to an acual view/page
    /// </summary>
    public static class ApplicationPageHelpers
    {

        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, in any, and creats desired page 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            switch (page)
            {
                case ApplicationPage.Home:
                    return new HomePage(viewModel as HomeViewModel);
                case ApplicationPage.LastShows:
                    return new LastShowsPage(viewModel as LastShowsViewModel);
                case ApplicationPage.UserPlayList:
                    return new UserPlayListPage(viewModel as UserPlayListViewModel);
                case ApplicationPage.PodcastPlaylist:
                    return new PodcastPlayListPage(viewModel as PodcastPlayListViewModel);
                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts <see cref="BasePage"/> to specefic <see cref="ApplicationPage"/> that is for the type of page 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find applicationPage that matches the page 
            if (page is HomePage)
                return ApplicationPage.Home;

            if (page is LastShowsPage)
                return ApplicationPage.LastShows;

            if (page is UserPlayListPage)
                return ApplicationPage.UserPlayList;

            if (page is PodcastPlayListPage)
                return ApplicationPage.PodcastPlaylist;
            // Alert the developer of the issue
            Debugger.Break();

            return default;
        }
    }
}
