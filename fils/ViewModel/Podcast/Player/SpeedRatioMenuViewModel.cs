using System.Collections.Generic;

namespace RadioArchive
{
    public class SpeedRatioMenuViewModel : MenuViewModel
    {
        public SpeedRatioMenuViewModel()
        {
            Items = new List<MenuItemViewModel>()
            {
                new MenuItemViewModel(){Text="Normal", SelectCommand= new RelayCommand(() => SetRatio(1))},
                new MenuItemViewModel(){Text="1.25", SelectCommand= new RelayCommand(() => SetRatio(1.25f))},
                new MenuItemViewModel(){Text="1.5", SelectCommand= new RelayCommand(() => SetRatio(1.5f))},
                new MenuItemViewModel(){Text="1.75", SelectCommand= new RelayCommand(() => SetRatio(1.75f))},
                new MenuItemViewModel(){Text="2", SelectCommand= new RelayCommand(() => SetRatio(2f))},
            };
        }

        private void SetRatio(float ratio)
        {
            // TODO Make Selected item Checked 
            DI.ViewModelPodcastPlayer.ChangeRatio(ratio);
            DI.ViewModelPodcastPlayer.SpeedRatioMenuVisible = false;
        }
    }
}
