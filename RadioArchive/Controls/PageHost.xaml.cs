using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {

        #region Dependency properties
        /// <summary>
        /// the currrentPage to show in page host
        /// </summary>
        public ApplicationPage CurrentPage
        {
            get => (ApplicationPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        /// <summary>
        /// returns <see cref="CurrentPage"/> as a property
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(ApplicationPage), typeof(PageHost), new UIPropertyMetadata(default(ApplicationPage), null, CurrentPagePropertyChanged));


        /// <summary>
        /// the currrentPage to show in page host
        /// </summary>
        public BaseViewModel CurrentPageViewModel
        {
            get => (BaseViewModel)GetValue(CurrentPageViewModelProperty);
            set => SetValue(CurrentPageViewModelProperty, value);
        }

        /// <summary>
        /// returns <see cref="CurrentPageViewModel"/> as a property
        /// </summary>
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel), typeof(BaseViewModel), typeof(PageHost), new UIPropertyMetadata());

        #endregion

        #region Constructor
        public PageHost()
        {
            InitializeComponent();

            // If we are in designing mod show the current page 
            // As the dependency property not fire 
            if (DesignerProperties.GetIsInDesignMode(this))
                NewPage.Content = new HomePage(new HomeViewModel());
        }
        #endregion

        #region property changed event

        /// <summary>
        /// Called when the <see cref="CurrentPage"/> value has changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            // Get current values
            var currentPage = (ApplicationPage)d.GetValue(CurrentPageProperty);
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            //get the frames
            var oldPageFrame = (d as PageHost).OldPage;
            var newPageFrame = (d as PageHost).NewPage;

            // If the current page hasn't changed 
            // Just update this view model 
            if (newPageFrame.Content is BasePage page && page.ToApplicationPage() == currentPage)
            {
                // Just update the view model 
                page.ViewModelObject = currentPageViewModel;

                return value;
            }

            //store the old page content
            var oldPageContent = newPageFrame.Content;

            //remove the current page from new page frame
            newPageFrame.Content = null;

            //move the previous page frame to the old page frame
            oldPageFrame.Content = oldPageContent;

            //animate out when the loaded event has called
            //right after this call due moving frames
            if (oldPageContent is BasePage oldPage)
            {
                //animate out the page
                oldPage.ShouldAnimateOut = true;

                //remove the content after animation done
                Task.Delay((int)(oldPage.SlideSeconds * 1000)).ContinueWith((p) =>
                {
                    Application.Current.Dispatcher.Invoke(() => oldPage.Content = null);
                });
            }

            //set up new page content
            newPageFrame.Content = currentPage.ToBasePage(currentPageViewModel);

            return value;
        }
        #endregion
    }
}
