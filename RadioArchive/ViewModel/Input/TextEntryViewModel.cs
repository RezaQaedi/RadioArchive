using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RadioArchive
{
    /// <summary>
    /// View model for text entry to edit a string value
    /// </summary>
    public class TextEntryViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// the label to identify what this value is for
        /// </summary>
        public string Lable { get; set; }
        /// <summary>
        /// the current saved value
        /// </summary>
        public string OriginalText { get; set; }
        /// <summary>
        /// the current none-commit edited text
        /// </summary>
        public string EditedText { get; set; }
        /// <summary>
        /// indicates if the current text is in edit mod
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// The action to run when saving the text,
        /// Returns true if commit was successful 
        /// </summary>
        public Func<Task<bool>> CommitAction { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// puts the control in edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// cancel out of edit mod
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// commits the edits and save the value
        /// as well as goes back to non edit mod
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TextEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }
        #endregion

        #region Commands Methods

        /// <summary>
        /// puts the control in edit mode
        /// </summary>
        public void Edit()
        {
            //set the edited text to the current value 
            EditedText = OriginalText;

            //go into editing mod
            Editing = true;
        }

        /// <summary>
        /// cancel out of edit mod
        /// </summary>
        public void Cancel()
        {
            Editing = false;
        }

        /// <summary>
        /// commits the edits and save the value
        /// as well as goes back to non edit mod
        /// </summary>
        public void Save()
        {
            // Store the result of commit data
            var result = default(bool);

            // Save currently value
            var currentSavedValue = OriginalText;

            RunCommand(() => Working, async () =>
            {
                // While working come out of editing mod
                Editing = false;

                // Commit the changed text so we can see it 
                OriginalText = EditedText;

                // Try and do the committing 
                result = CommitAction == null ? false : await CommitAction.Invoke();
            }).ContinueWith(t =>
            {
                // If we failed 
                if (!result)
                {
                    // Set back the original text                    
                    OriginalText = currentSavedValue;

                    // Get user into editing mod again
                    Editing = true;
                }
            });
        }

        #endregion
    }
}
