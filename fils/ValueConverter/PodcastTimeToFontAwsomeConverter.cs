using System;
using System.Globalization;

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
            var fontAwsome = "\uf2ce";

            switch (podcastTime)
            {
                case PodcastTime.Evening:
                    fontAwsome = "\uf186";
                    break;
                case PodcastTime.Morning:
                    fontAwsome = "\uf185";
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
