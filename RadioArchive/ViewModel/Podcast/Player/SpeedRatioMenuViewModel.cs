using System.Collections.Generic;

namespace RadioArchive
{
    public class SpeedRatioMenuViewModel : MenuViewModel
    {
        public SpeedRatioMenuViewModel()
        {
            Items = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel(){Text="Normal", IsSelected = true, SelectCommand= new RelayCommand(() => SetRatio(1)), Data = 1f},
                new MenuItemViewModel(){Text="x1.25", SelectCommand= new RelayCommand(() => SetRatio(1.25f)), Data = 1.25f},
                new MenuItemViewModel(){Text="x1.5", SelectCommand= new RelayCommand(() => SetRatio(1.5f)), Data = 1.5f},
                new MenuItemViewModel(){Text="x1.75", SelectCommand= new RelayCommand(() => SetRatio(1.75f)), Data = 1.75f},
                new MenuItemViewModel(){Text="x2", SelectCommand= new RelayCommand(() => SetRatio(2f)), Data = 2f},
            };
        }

        private void SetRatio(float ratio)
        {
            // Let player know of this change
            DI.ViewModelPodcastPlayer.ChangeRatio(ratio);
            DI.ViewModelPodcastPlayer.SpeedRatioMenuVisible = false;

            foreach (var item in Items)
            {
                var itemRaito = (float)item.Data;
                item.IsSelected = itemRaito == ratio;
            }
        }
    }
}
