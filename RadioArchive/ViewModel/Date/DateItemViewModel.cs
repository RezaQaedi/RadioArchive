using System;
using System.Windows.Input;

namespace RadioArchive
{
    public class DateItemViewModel : BaseViewModel
    {
        /// <summary>
        /// Title of this Podcast
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Value of this item 
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Command for selecting this item 
        /// </summary>
        public ICommand SelectCommand { get; set; }

        /// <summary>
        /// Action for selecting this item 
        /// </summary>
        public Action<int> SelectAction { get; set; }

        public DateItemViewModel()
        {
            SelectCommand = new RelayCommand(() => SelectAction?.Invoke(Value));
        }
    }
}
