using System;
using System.Globalization;

namespace RadioArchive
{
    /// <summary>
    /// converter takes in Time span and converts to user friendly time  
    /// </summary>
    public class TimeSpanToDisplayTimeConverter : BaseValueConverter<TimeSpanToDisplayTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            //get the time
            var timeSpan = (TimeSpan)value;

            if (timeSpan.Hours > 0)
                return timeSpan.ToString(@"hh\:mm\:ss");
            else
                return timeSpan.ToString(@"mm\:ss");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
