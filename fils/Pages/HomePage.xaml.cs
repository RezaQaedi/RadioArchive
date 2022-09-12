namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : BasePage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public HomePage(HomeViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
