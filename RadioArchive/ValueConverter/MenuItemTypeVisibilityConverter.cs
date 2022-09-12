using System;
using System.Globalization;
using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// converter takes <see cref="MenuItemType"/> and return <see cref="Visibility"/> base on the 
    /// given parameter being the same as the type
    /// </summary>
    public class MenuItemTypeVisibilityConverter : BaseValueConverter<MenuItemTypeVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if we have no parameter return invisible
            if (parameter == null)
                return Visibility.Collapsed;

            //try and convert parameter string to enum
            if (!Enum.TryParse(parameter as string, out MenuItemType type))
                return Visibility.Collapsed;

            //return if the parameter matches the type 
            return (MenuItemType)value == type ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
