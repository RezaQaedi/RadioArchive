using System.Threading.Tasks;

namespace RadioArchive
{
    /// <summary>
    /// the UI manage that controls any UI interaction in application
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// Get User input 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<string> ShowGetUserInputDiolgBox(GetUserInputDialogViewModel viewModel);

        /// <summary>
        /// Show info box 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public Task ShowInfoDiolgBox(InfoDilogViewModel viewModel);
    }
}
