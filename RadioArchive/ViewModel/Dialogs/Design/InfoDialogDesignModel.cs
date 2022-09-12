namespace RadioArchive
{
    /// <summary>
    /// DesignModel for <see cref="InfoDilogViewModel"/>
    /// </summary>
    public class InfoDialogDesignModel : InfoDilogViewModel
    {
        #region Singleton
        /// <summary>
        /// a singleton instance of design model
        /// </summary>
        public static InfoDialogDesignModel Instance => new InfoDialogDesignModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public InfoDialogDesignModel() => (Message, AcceptText) = ("Dialog box Message Here", "Accept");
        #endregion
    }
}
