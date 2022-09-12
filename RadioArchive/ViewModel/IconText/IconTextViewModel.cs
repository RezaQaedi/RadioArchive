using System.Windows.Input;

namespace RadioArchive
{
    public class IconTextViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        /// Text to display
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Inidcates if this item can be removed 
        /// </summary>
        public bool IsRemoveble { get; set; }

        /// <summary>
        /// Icon to dispay 
        /// </summary>
        public IconType Icon { get; set; }
        #endregion

        /// <summary>
        /// Command for selecting this item 
        /// </summary>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        /// Command for removing this item 
        /// </summary>
        public ICommand RemoveCommand { get; set; }
    }
}
