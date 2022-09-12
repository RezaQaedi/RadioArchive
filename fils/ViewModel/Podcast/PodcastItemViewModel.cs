using System;
using System.Windows.Media;

namespace RadioArchive
{
    public class PodcastItemViewModel : BaseViewModel
    {
        /// <summary>
        /// View Model for Morning show even if the time not specified pass it as morning 
        /// </summary>
        public PodcastViewModel MorningShow { get; set; }

        /// <summary>
        /// View Model for evening show 
        /// </summary>
        public PodcastViewModel EveningShow { get; set; }

        /// <summary>
        /// Get's if this podcast has morning version 
        /// </summary>
        public bool HasMorning => MorningShow != null && NoSpecificTime == false;

        /// <summary>
        /// Get's if this podcast has evening version 
        /// </summary>
        public bool HasEvening => EveningShow != null && NoSpecificTime == false;

        /// <summary>
        /// Get's if this podcast dosnt have any specific date 
        /// </summary>
        public bool NoSpecificTime => EveningShow == null && MorningShow.NoSpecificDate;

        /// <summary>
        /// Get's if this podcast has only one time or dosnt specified at all 
        /// </summary>
        public bool HasOnlyOneTime => (MorningShow == null || EveningShow == null) || NoSpecificTime;

        /// <summary>
        /// Title of this Podcast
        /// </summary>
        public string DisplayTitle => Date.ToString("dddd");

        /// <summary>
        /// The date of relase 
        /// </summary>
        public DateTimeOffset Date
        {
            get
            {
                if (MorningShow != null) 
                    return MorningShow.Date;
                else
                    return EveningShow.Date;
            }
        }

        /// <summary>
        /// Color of background 
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        ///  50% Lighter version of <see cref="BackgroundColor"/>
        /// </summary>
        public Color BackgroundColorLighter => BackgroundColor.Lerp(Colors.White, 0.5f);

        public PodcastItemViewModel(Color backgroundColor, PodcastViewModel morning = null, PodcastViewModel evening = null)
        {
            MorningShow = morning;
            EveningShow = evening;
            BackgroundColor = backgroundColor;
        }
    }
}
