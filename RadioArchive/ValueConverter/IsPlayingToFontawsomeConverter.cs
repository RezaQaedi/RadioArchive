using System;
using System.Globalization;

namespace RadioArchive
{
    /// <summary>
    /// converter takes <see cref="IconType"/> and return FontAwesome
    /// </summary>
    public class IsPlayingToFontawsomeConverter : BaseValueConverter<IsPlayingToFontawsomeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? IconType.Pause.ToFontAwesome() : IconType.Play.ToFontAwesome();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
