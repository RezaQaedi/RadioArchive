using System;
using System.Globalization;

namespace RadioArchive
{
    /// <summary>
    /// Takes <see cref="PodcastTime"/> and retruns specefied text to show as icon tooltip
    /// </summary>
    public class PodcastTimeToToolTipTextConverter : BaseValueConverter<PodcastTimeToToolTipTextConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var podcastTime = (PodcastTime)value;
            var tooltipStr = "None";

            switch (podcastTime)
            {
                case PodcastTime.Evening:
                    tooltipStr = "Evening show";
                    break;
                case PodcastTime.Morning:
                    tooltipStr = "Morning Show";
                    break;
            }

            return tooltipStr;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
