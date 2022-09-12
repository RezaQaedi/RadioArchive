using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RadioArchive
{
    public class LastShowsViewModel : BaseViewModel
    {
        #region Private filds 
        private const int MAXYEAR = 2022;
        private const int MINYEAR = 2007;

        private int _SelectedMonth;
        private int _SelectedYear;

        private readonly ObservableCollection<DateItemViewModel> _years;
        private readonly ObservableCollection<DateItemViewModel> _month; 
        #endregion

        #region Public properties 
        /// <summary>
        /// Title of this List
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Indicates if years has been selected 
        /// </summary>
        public bool IsYearSelected { get; set; }

        /// <summary>
        /// Command for backing up to selecting years 
        /// </summary>
        public ICommand BackToYears { get; set; }

        /// <summary>
        /// Observable list of <see cref="DateItemViewModel"/>
        /// </summary>
        public ObservableCollection<DateItemViewModel> Items { get; set; } = new ObservableCollection<DateItemViewModel>();

        #endregion

        #region Counstractor
        public LastShowsViewModel()
        {
            _years = new();
            _month = new();

            // Add dates  
            foreach (var yearDateViewModel in GetYears())
                _years.Add(yearDateViewModel);
            foreach (var monthDateViewModel in GetMonths())
                _month.Add(monthDateViewModel);

            // Add years by default 
            LoadYears();

            // Create commands
            BackToYears = new RelayCommand(LoadYears);
        } 
        #endregion

        #region Private Methods  

        /// <summary>
        /// Add years and set year flags as not selected 
        /// </summary>
        private void LoadYears()
        {
            Items = _years;
            DisplayTitle = "Over 15 years";
            IsYearSelected = false;
        }

        /// <summary>
        /// Gets list of <see cref="DateItemViewModel"/> for Invariant Years
        /// </summary>
        private IEnumerable<DateItemViewModel> GetYears()
        {
            List<DateItemViewModel> years = new();
            for (int i = MAXYEAR; i >= MINYEAR; i--)
            {
                var item = new DateItemViewModel()
                {
                    Value = i,
                    DisplayTitle = i.ToString(),
                };

                item.SelectAction = SelectYear;

                _years.Add(item);
            }

            return years;
        }

        /// <summary>
        /// Gets list of <see cref="DateItemViewModel"/> for Invariant months
        /// </summary>
        private IEnumerable<DateItemViewModel> GetMonths()
        {
            List<DateItemViewModel> months = new(); 
            for (int i = 1; i <= 12; i++)
            {
                var item = new DateItemViewModel()
                {
                    Value = i,
                    DisplayTitle = TimeHelper.GetInvariantMonthName(i),
                };

                item.SelectAction = SelectMonth;

                months.Add(item);
            }

            return months;
        }



        #endregion

        #region Public methods
        /// <summary>
        /// Sets given month as selected and Opens page to selected date 
        /// </summary>
        /// <param name="month">Month to Select</param>
        public void SelectMonth(int month)
        {
            _SelectedMonth = month;
            // Navigate to podcastList 
            var PodcastPlayListVM = new PodcastPlayListViewModel(_SelectedYear, _SelectedMonth);
            PodcastPlayListVM.LoadAsync();

            DI.ViewModelApplication.GoToPage(ApplicationPage.PodcastPlaylist, PodcastPlayListVM);
        }

        /// <summary>
        /// Set given year as selected and loads the months
        /// </summary>
        /// <param name="year">Year to select</param>
        public void SelectYear(int year)
        {
            DisplayTitle = year.ToString();
            _SelectedYear = year;
            IsYearSelected = true;
            Items = _month;
        } 
        #endregion
    }
}
