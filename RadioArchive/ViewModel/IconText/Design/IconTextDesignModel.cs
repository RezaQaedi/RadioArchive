namespace RadioArchive
{
    /// <summary>
    /// the design-time view model for the <see cref="MenuItemViewModel"/>  
    /// </summary>
    public class IconTextDesignModel : IconTextViewModel
    {
        #region Singleton
        /// <summary>
        /// a singleton instance of design model
        /// </summary>
        public static IconTextDesignModel Instance => new IconTextDesignModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public IconTextDesignModel() => (DisplayText, Icon) = ("Hello World", IconType.Play);        

        #endregion
    }
}
