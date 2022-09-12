using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace RadioArchive
{
    public abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter where T : class, new ()
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

        #region Value Converter Methods

        /// <summary>
        /// Method to convert the a type to another
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Method to convert back a type to its surce type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);

        #endregion
    }
}
