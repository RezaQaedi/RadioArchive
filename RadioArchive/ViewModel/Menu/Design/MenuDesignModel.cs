using System.Collections.Generic;
using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// the design-time view model for the <see cref="MenuViewModel"/>  
    /// </summary>
    public class MenuDesignModel : MenuViewModel
    {
        #region Singleton
        /// <summary>
        /// a singleton instance of design model
        /// </summary>
        public static MenuDesignModel Instance => new MenuDesignModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuDesignModel()
        {
            Items = new List<MenuItemViewModel>
            {
                new MenuItemViewModel(){Text="Design Time header", Type = MenuItemType.Header},
                new MenuItemViewModel(){Text="Menu item 1", Icon = IconType.File},
                new MenuItemViewModel(){Text="Menu item 2", Icon = IconType.Picture},
            };
        }

        #endregion
    }
}
