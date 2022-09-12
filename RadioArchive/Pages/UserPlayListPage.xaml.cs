namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for UserPlayListpage 
    /// </summary>
    public partial class UserPlayListPage : BasePage<UserPlayListViewModel>
    {
        public UserPlayListPage()
        {
            InitializeComponent();
        }

        public UserPlayListPage(UserPlayListViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
