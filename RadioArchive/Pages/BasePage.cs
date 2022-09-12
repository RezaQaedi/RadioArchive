using Dna;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RadioArchive
{
    /// <summary>
    /// a base class for pages to gain base functionality
    /// </summary>
    public class BasePage : UserControl
    {
        #region Private members

        //the view model associated with this page
        private object mViewModel;

        #endregion

        #region Public properties
        /// <summary>
        /// the animation that play when page is loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlidAndFadeInFromTheRight;

        /// <summary>
        /// the animation that play when page is unloaded
        /// </summary>
        public PageAnimation PageUnLoadAnimation { get; set; } = PageAnimation.SlidAndFadeOutTotheLeft;

        /// <summary>
        /// the time any animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8f;

        /// <summary>
        /// flag that indicates this page should animate out on load,
        /// useful for moving page to another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// View model associated with this page 
        /// </summary>
        public object ViewModelObject
        {
            get => mViewModel;
            set
            {
                //if value is the same do nothing
                if (mViewModel == value)
                    return;

                // Fire the on View model change method
                OnViewModelChanged();

                //update the value
                mViewModel = value;

                //set the data context for this page
                this.DataContext = mViewModel;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            //no bother animating in designer mod
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            //if we are animating in hide begin with
            if (this.PageLoadAnimation != PageAnimation.None)
                Visibility = Visibility.Collapsed;

            //listen out for the page loading
            this.Loaded += BasePage_Loaded;
        }

        #endregion

        #region Animation load/unload event

        /// <summary>
        /// once the page loaded perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //if we are setup to animate out on load
            if (ShouldAnimateOut)
                //animate out
                await AnimateOut();
            else
                //animate page in
                await AnimateIn();
        }

        /// <summary>
        /// animate the page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateIn()
        {
            // making sure we have something to do
            if (this.PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlidAndFadeInFromTheRight:
                    //start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, SlideSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
            }
        }

        /// <summary>
        /// animate the page out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOut()
        {
            // making sure we have something to do
            if (this.PageUnLoadAnimation == PageAnimation.None)
                return;

            switch (PageUnLoadAnimation)
            {
                case PageAnimation.SlidAndFadeOutTotheLeft:
                    //start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, SlideSeconds);
                    break;
            }
        }
        #endregion

        protected virtual void OnViewModelChanged() { }
    }


    /// <summary>
    /// a base page we added view model support
    /// </summary>
    public class BasePage<T> : BasePage where T : BaseViewModel, new()
    {

        #region Public propertes

        /// <summary>
        /// View model associated with this page 
        /// </summary>
        public T ViewModel
        {
            get => (T)ViewModelObject;
            set => ViewModelObject = value;
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage() : base()
        {
            // If we are in design mod just user new instance of T
            if (DesignerProperties.GetIsInDesignMode(this))
                ViewModel = new T();
            else
                //create a default view model
                ViewModel = Framework.Service<T>() ?? new T();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="specceficViewModel">The specific view model to use if have any</param>
        public BasePage(T speceficViewModel = null) : base()
        {
            if (speceficViewModel != null)
                ViewModel = speceficViewModel;
            else
            {
                // If we are in design mod just user new instance of T
                if (DesignerProperties.GetIsInDesignMode(this))
                    ViewModel = new T();
                else
                    //create a default view model
                    ViewModel = Framework.Service<T>() ?? new T();
            }
        }

        #endregion
    }
}
