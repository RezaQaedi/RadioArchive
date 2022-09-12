using System.Threading.Tasks;

namespace RadioArchive
{
    public class UserPlayListViewModel : BaseViewModel
    {
        /// <summary>
        /// <see cref="IconTextViewModel"/> of items 
        /// </summary>
        public IconTextListViewModel UserCreatedPlaylist { get; set; }

        public UserPlayListViewModel()
        {
            UserCreatedPlaylist = new IconTextListViewModel();

            // Add default items 
            UserCreatedPlaylist.Items.Add(new IconTextViewModel() { DisplayText = "Liked", Icon = IconType.Heart, SelectCommand = new RelayCommand(() => DI.ViewModelApplication.ShowLikedList()) });
            UserCreatedPlaylist.Items.Add(new IconTextViewModel() { DisplayText = "History", Icon = IconType.History, SelectCommand = new RelayCommand(() => DI.ViewModelApplication.ShowUserHistoryList()) });
            UserCreatedPlaylist.Items.Add(new IconTextViewModel() { DisplayText = "", Icon = IconType.Add, SelectCommand = new RelayCommand(() => CreateNewPlayList()) });

            // Add already created items 
            foreach (var item in DI.StorgeService.GetUserList())
            {
                AddNewUserCreatedItem(item.Title);
            }
        }

        #region Private methods 
        /// <summary>
        /// Adds new item by getting name from user 
        /// </summary>
        private async void CreateNewPlayList()
        {
            var userInput = await DI.UI.ShowGetUserInputDiolgBox(new GetUserInputDialogViewModel() { Title = "Type a name", AcceptText = "Save" });

            if (string.IsNullOrEmpty(userInput))
                return;

            userInput = userInput.Trim();
            var respond = false;

            await RunCommand(() => Working, async () => 
            {
                await Task.Run(() => 
                { 
                    respond = DI.StorgeService.CreateNewList(userInput);                    
                });
            });

            if (respond)
                // Create new item 
                AddNewUserCreatedItem(userInput);
            else
            {
                await DI.UI.ShowInfoDiolgBox(new InfoDilogViewModel() { AcceptText = "ok", Message = $"{userInput} already exist", Title = "Failed to add new playlist" });
            }
        }

        private void AddNewUserCreatedItem(string title)
        {
            var iconText = new IconTextViewModel()
            {
                DisplayText = title,
                Icon = IconType.UserList,
                IsRemoveble = true,
                SelectCommand = new RelayCommand(() => DI.ViewModelApplication.ShowUserCreatedList(title)),
            };

            iconText.RemoveCommand =
                 new RelayCommand(() => RemoveList(iconText));

            UserCreatedPlaylist.Items.Insert(UserCreatedPlaylist.Items.Count - 1, iconText);
        }

        /// <summary>
        /// Removes item by given title 
        /// </summary>
        /// <param name="Title"></param>
        private async void RemoveList(IconTextViewModel iconText)
        {
            await RunCommand(() => Working, async () =>
            {
                await Task.Run(() => DI.StorgeService.RemoveList(iconText.DisplayText));
            });

            UserCreatedPlaylist.Items.Remove(iconText);
        }
        #endregion
    }
}
