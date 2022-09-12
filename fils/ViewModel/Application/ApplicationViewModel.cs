using System.Windows.Input;
using LibVLCSharp.Shared;
using System.Linq;
using System;
using System.Collections.Generic;

namespace RadioArchive
{
    /// <summary>
    /// the application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public Properties

        public ICommand GoHomePageCommand { get; set; }

        public ICommand GoLastShowsPageCommand { get; set; }

        /// <summary>
        /// the current page of application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        /// <summary>
        /// The view model to use for the current page when current page changes
        /// NOTE : this is not the Live Up To Date view model for the current page 
        ///        its simply used to set the view model of the current page at the
        ///        time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if side menu should be visible
        /// </summary>
        public bool SideMenuVisible { get; set; } = true;

        #endregion

        #region Constructor
        public ApplicationViewModel()
        {
            // Creating commands 
            GoHomePageCommand = new RelayCommand(() => GoToPage(ApplicationPage.Home));
            GoLastShowsPageCommand = new RelayCommand(() => GoToPage(ApplicationPage.LastShows));
        }
        #endregion

        #region Public Methods 

        /// <summary>
        /// navigates to specified page
        /// </summary>
        /// <param name="viewModel">The view model if any, set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // If we already are in this page do nothing 
            if (CurrentPage == page)
                return;

            //set the current page
            CurrentPage = page;

            // Set the view model
            CurrentPageViewModel = viewModel;

            // Fire off a current page changed event
            OnPropertyChanged(nameof(CurrentPage));
        }
        #endregion
    }
}
