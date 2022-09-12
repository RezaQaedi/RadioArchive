namespace RadioArchive.Pages
{
    /// <summary>
    /// Interaction logic for PlayListPage.xaml
    /// </summary>
    public partial class PlayListPage : BasePage<HomeViewModel>
    {
        public PlayListPage()
        {
            InitializeComponent();
        }

        public PlayListPage(HomeViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
