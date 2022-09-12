using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RadioArchive
{
    public class WpfMenuItemViewModel
    {
        public event Action<WpfMenuItemViewModel> SubMenuOpend;

        private bool _IsSubMenuOpen;
        public string Header { get; set; }

        public IconType Icon { get; set; }

        public bool IsSubMenuOpen { get => _IsSubMenuOpen;
            set 
            {
                _IsSubMenuOpen = value;
                if(value)
                    SubMenuOpend?.Invoke(this);
            }
        }

        public ObservableCollection<WpfMenuItemViewModel> MenuItems { get; set; }

        public ICommand Command { get; set; }
    }
}
