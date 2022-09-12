using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RadioArchive
{
    /// <summary>
    /// Base class for any content wants to be using in <see cref="DialogWindow"/>
    /// </summary>
    public class BaseDialogUserControl : UserControl
    {
        #region Private Members

        protected DialogWindow mDialogWindow;

        #endregion

        #region Public Properties

        /// <summary>
        /// the minimum height of this dialog
        /// </summary>
        public int WindowMinimumHeight { get; set; } = 100;

        /// <summary>
        /// the minimum width of this dialog 
        /// </summary>
        public int WindowMinimumWidth { get; set; } = 250;

        /// <summary>
        /// the height of the title bar
        /// </summary>
        public int TitleHeight { get; set; } = 30;

        /// <summary>
        /// the title of the dialog
        /// </summary>
        public string Title { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// close the dialog
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        #endregion

        #region Contractor

        /// <summary>
        /// Default contractor
        /// </summary>
        public BaseDialogUserControl()
        {
            //creating commands
            CloseCommand = new RelayCommand(() => mDialogWindow.Close());

            //create a new dialog window
            mDialogWindow = new DialogWindow();
            mDialogWindow.ViewModel = new DialogWindowViewModel(mDialogWindow);
        }

        #endregion

        #region Public dialog Show Methods

        /// <summary>
        /// Display single message box to user
        /// </summary>
        /// <param name="T">the view model</param>
        /// <returns></returns>
        public Task ShowDialog<T>(T viewModel)
            where T: BaseDialogViewModel
        {
            var tsc = new TaskCompletionSource<bool>();

            //run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    //match controls expected size for the dialog view model
                    mDialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    mDialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    mDialogWindow.ViewModel.TitleHeight= TitleHeight;
                    mDialogWindow.ViewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title;

                    //set up this data context binding to the view model
                    DataContext = viewModel;

                    //set up this control data content binding to the view model
                    mDialogWindow.ViewModel.Content = this;

                    // Show in the Center of the parent 
                    mDialogWindow.Owner = Application.Current.MainWindow;
                    mDialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    //show dialog 
                    mDialogWindow.ShowDialog();
                }
                finally
                {
                    //Let caller know we finished
                    tsc.TrySetResult(true);
                }
            });

            return tsc.Task;
        }
        #endregion
    }
}
