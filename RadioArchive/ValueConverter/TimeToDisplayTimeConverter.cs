using System;
using System.Globalization;

namespace RadioArchive
{
    /// <summary>
    /// converter takes in date and converts to user friendly time 
    /// </summary>
    public class TimeToDisplayTimeConverter : BaseValueConverter<TimeToDisplayTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //get the time
            var time = (DateTimeOffset)value;
            var Diffrence = DateTimeOffset.UtcNow - time;

            if (Diffrence.Days <= 7)
            {
                if (Diffrence.Days == 0)
                    return "Today";

                return $"{Diffrence.Days} Day ago";
            }

            //otherwise, return a full date
            return time.ToLocalTime().ToString("yyy/MM/dd");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
