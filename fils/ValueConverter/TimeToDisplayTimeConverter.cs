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

            //otherwise, return a full date
            return time.ToLocalTime().ToString("yyy/MM/dd");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
