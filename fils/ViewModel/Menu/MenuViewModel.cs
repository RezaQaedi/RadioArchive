using System.Collections.Generic;

namespace RadioArchive
{
    /// <summary>
    /// View model for a menu
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        /// <summary>
        /// the items in this menu
        /// </summary>
        public List<MenuItemViewModel> Items { get; set; }
    }
}
