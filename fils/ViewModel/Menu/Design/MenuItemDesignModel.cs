using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// the design-time view model for the <see cref="MenuItemViewModel"/>  
    /// </summary>
    public class MenuItemDesignModel : MenuItemViewModel
    {
        #region Singleton
        /// <summary>
        /// a singleton instance of design model
        /// </summary>
        public static MenuItemDesignModel Instance => new MenuItemDesignModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuItemDesignModel() => (Text, Icon) = ("Hello World", IconType.File);        

        #endregion
    }
}
