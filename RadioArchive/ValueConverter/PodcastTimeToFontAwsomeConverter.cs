using System;
using System.Globalization;
using RadioArchive.Core;

namespace RadioArchive
{
    /// <summary>
    /// Takes <see cref="PodcastTime"/> and retruns specefied Fontaswome as for display
    /// </summary>
    public class PodcastTimeToFontAwsomeConverter : BaseValueConverter<PodcastTimeToFontAwsomeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var podcastTime = (PodcastTime)value;
            var fontAwsome = IconType.Podcast.ToFontAwesome();

            switch (podcastTime)
            {
                case PodcastTime.Evening:
                    fontAwsome = IconType.Evening.ToFontAwesome();
                    break;
                case PodcastTime.Morning:
                    fontAwsome = IconType.Morning.ToFontAwesome();
                    break; 
                case PodcastTime.Afternoon:
                    fontAwsome = IconType.Afternoon.ToFontAwesome();
                    break;
            }

            return fontAwsome;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
