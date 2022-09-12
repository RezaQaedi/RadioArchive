using System.Threading.Tasks;

namespace RadioArchive
{
    /// <summary>
    /// the application implementation of <see cref="IUIManager"/>
    /// </summary>
    class UIManger : IUIManager
    {
        /// <summary>
        /// Show dilog box gets user input 
        /// </summary>
        /// <param name="viewModel">the view model</param>
        /// <returns></returns>
        public Task<string> ShowGetUserInputDiolgBox(GetUserInputDialogViewModel viewModel)
        {
            return new DialogGetUserInput().ShowDialog(viewModel);
        }

        /// <summary>
        /// Show dilog box gets user input 
        /// </summary>
        /// <param name="viewModel">the view model</param>
        /// <returns></returns>
        public Task ShowInfoDiolgBox(InfoDilogViewModel viewModel)
        {
            return new DialogInfo().ShowDialog(viewModel);
        }
    }
}
