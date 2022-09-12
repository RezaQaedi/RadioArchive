using System.Windows;
using System.Windows.Controls;

namespace RadioArchive
{
    /// <summary>
    /// View model for Custom Flat Window
    /// </summary>
    public class DialogWindowViewModel : WindowViewModel
    {
        #region Public Properties

        /// <summary>
        /// the title of this dialog window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the content to host inside the dialog
        /// </summary>
        public Control Content { get; set; }
        #endregion

        #region Contractor
        public DialogWindowViewModel(Window window) : base(window) 
        {
            //make min height smaller
            WindowMinimumHeight = 100;
            WindowMinimumWidth = 250;

            // Let other know this is sub window
            IsMainWindow = false;

            //make title bar smaller
            TitleHeight = 30;
        }
        #endregion

    }
}
