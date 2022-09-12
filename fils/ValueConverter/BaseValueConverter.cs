using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace RadioArchive
{
    /// <summary>
    /// a base vale converter that allows direct XAML usage
    /// </summary>
    /// <typeparam name="T">the type of the value converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T :class, new()
    {
        #region private members
        //a single value instance of this value converter
        private static T mConverter = null;

        #endregion

        #region markup extention methods
        /// <summary>
        /// provid a static instance of the value converter
        /// </summary>
        /// <param name="serviceProvider">the service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        } 
        #endregion

        #region value converter methods
        /// <summary>
        /// the method to convert the a type to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// the method to convert back a type to its surce type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture); 
        #endregion
    }
}
