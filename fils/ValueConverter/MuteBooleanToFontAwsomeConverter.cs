using System;
using System.Globalization;

namespace RadioArchive
{
    /// <summary>
    /// Takes <see cref="bool"/> and retruns specefied Fontaswome as for sound icon
    /// </summary>
    public class MuteBooleanToFontAwsomeConverter : BaseValueConverter<MuteBooleanToFontAwsomeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "\uf6a9" : "\uf028";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
