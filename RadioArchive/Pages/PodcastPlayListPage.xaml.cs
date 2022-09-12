using System.Windows.Media;

namespace RadioArchive
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class PodcastPlayListPage : BasePage<PodcastPlayListViewModel>
    {
        public PodcastPlayListPage()
        {
            InitializeComponent();
        }

        public PodcastPlayListPage(PodcastPlayListViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
