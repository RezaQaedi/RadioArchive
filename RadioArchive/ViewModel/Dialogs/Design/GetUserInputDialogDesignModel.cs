namespace RadioArchive
{
    /// <summary>
    /// DesignModel for <see cref="MessageBoxDialogViewModel"/>
    /// </summary>
    public class GetUserInputDialogDesignModel : GetUserInputDialogViewModel
    {
        #region Singleton
        /// <summary>
        /// a singleton instance of design model
        /// </summary>
        public static GetUserInputDialogDesignModel Instance => new GetUserInputDialogDesignModel();
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GetUserInputDialogDesignModel() => (Message, AcceptText, CancelText) = ("Dialog box Message Here", "Accept", "Cancel");
        #endregion
    }
}
