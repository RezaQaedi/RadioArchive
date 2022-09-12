using System;
using System.Globalization;
using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// converter takes boolin and return opacity
    /// </summary>
    public class BooleanToOpacityConverter : BaseValueConverter<BooleanToOpacityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
                return (bool)value ? 1f : 0.2f;
            else
                return (bool)value ? 0.2f : 1f;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
