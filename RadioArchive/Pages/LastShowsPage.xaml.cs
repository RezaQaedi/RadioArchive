namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class LastShowsPage : BasePage<LastShowsViewModel>
    {
        public LastShowsPage()
        {
            InitializeComponent();
        }

        public LastShowsPage(LastShowsViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
