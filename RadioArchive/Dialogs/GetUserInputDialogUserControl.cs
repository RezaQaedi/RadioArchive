using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RadioArchive
{
    /// <summary>
    /// Confirmation box that will be using in <see cref="DialogWindow"/>
    /// </summary>
    public class GetUserInputDialogUserControl : BaseDialogUserControl
    {
        #region Private Member

        private string mActionAccepted = null;

        #endregion

        #region Commands

        /// <summary>
        /// Accept the action Command
        /// </summary>
        public ICommand AcceptCommand { get; private set; }

        /// <summary>
        /// Cancel the action Command
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        #endregion

        #region Contractor

        /// <summary>
        /// Default contractor
        /// </summary>
        public GetUserInputDialogUserControl()
        {

            AcceptCommand = new RelayCommand(() => 
            {
                mActionAccepted = (DataContext as GetUserInputDialogViewModel).InputText;
                mDialogWindow.Close(); 
            });

            CancelCommand = new RelayCommand(() => mDialogWindow.Close());

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
        public Task<string> ShowDialog(GetUserInputDialogViewModel viewModel)
        {
            var tsc = new TaskCompletionSource<string>();

            //run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    //match controls expected size for the dialog view model
                    mDialogWindow.ViewModel.WindowMinimumHeight = WindowMinimumHeight;
                    mDialogWindow.ViewModel.WindowMinimumWidth = WindowMinimumWidth;
                    mDialogWindow.ViewModel.TitleHeight = TitleHeight;
                    mDialogWindow.ViewModel.Title = (string.IsNullOrEmpty(viewModel.Title) ? Title : viewModel.Title);

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
                    tsc.TrySetResult(mActionAccepted);
                }
            });

            return tsc.Task;
        }
        #endregion
    }
}
