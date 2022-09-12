using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        #region Private Members

        private DialogWindowViewModel mViewModel;

        #endregion

        #region Public Propertes

        /// <summary>
        /// the view model for the window
        /// </summary>
        public DialogWindowViewModel ViewModel
        {
            get => mViewModel;
            set
            {
                //set the new value 
                mViewModel = value;
                //update the data contex
                DataContext = mViewModel;
            }
        }

        #endregion
        #region Constractor

        /// <summary>
        /// default constractor
        /// </summary>
        public DialogWindow()
        {
            InitializeComponent();
        }
        #endregion
    }
}
