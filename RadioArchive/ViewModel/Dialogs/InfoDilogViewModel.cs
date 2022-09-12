namespace RadioArchive
{
    /// <summary>
    /// Detail for MessageBoxDialog
    /// </summary>
    public class InfoDilogViewModel : BaseDialogViewModel
    {
        /// <summary>
        /// the message to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Indicates if this dialog has any message 
        /// </summary>
        public bool HasAnyMessage => !string.IsNullOrEmpty(Message);

        /// <summary>
        /// the text to use in Accept button
        /// </summary>
        public string AcceptText { get; set; } = "ok";
    }
}
