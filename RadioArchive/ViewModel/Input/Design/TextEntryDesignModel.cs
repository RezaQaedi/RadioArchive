namespace RadioArchive
{
    /// <summary>
    /// DesignModel for <see cref="TextEntryViewModel"/>
    /// </summary>
    public class TextEntryDesignModel : TextEntryViewModel
    {
        #region Singleton
        /// <summary>
        /// a singleton instance of design model
        /// </summary>
        public static TextEntryDesignModel Instance => new();
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TextEntryDesignModel() => (OriginalText, Lable, EditedText) = ("Mehrad Ghaedi", "Name", "Editing Mod");
        #endregion
    }
}
