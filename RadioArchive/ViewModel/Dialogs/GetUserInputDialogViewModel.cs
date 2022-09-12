using System;

namespace RadioArchive
{
    /// <summary>
    /// Detail for MessageBoxDialog
    /// </summary>
    public class GetUserInputDialogViewModel : BaseDialogViewModel
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
        /// User input text 
        /// </summary>
        public string InputText { get; set; }

        /// <summary>
        /// the text to use in Accept button
        /// </summary>
        public string AcceptText { get; set; } = "Accept";

        /// <summary>
        /// the text to use in Cancel button
        /// </summary>
        public string CancelText { get; set; } = "Cancel";
    }
}
