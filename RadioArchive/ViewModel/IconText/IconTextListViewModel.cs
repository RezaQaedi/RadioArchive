using System.Collections.ObjectModel;

namespace RadioArchive
{
    public class IconTextListViewModel : BaseViewModel
    {
        public ObservableCollection<IconTextViewModel> Items { get; set; } = new ObservableCollection<IconTextViewModel>();
    }
}
