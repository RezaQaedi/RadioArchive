using System.Windows.Input;

namespace RadioArchive
{
    /// <summary>
    /// View model for a menu item
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        /// <summary>
        /// the text to display for menu item
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Select command for when item selected 
        /// </summary>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        /// the icon for the menu item
        /// </summary>
        public IconType Icon { get; set; }

        /// <summary>
        /// the type of this menu item
        /// </summary>
        public MenuItemType Type { get; set; }

        /// <summary>
        /// Indicates if this item is selected 
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Data object stored in this item 
        /// </summary>
        public object Data { get; set; }
    }
}
