using System;
using System.Collections;
using System.Globalization;

namespace RadioArchive
{
    public class CollectionToCollectionCountConverter : BaseValueConverter<CollectionToCollectionCountConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = value as IEnumerable;
            var count = 0;

            if (collection != null) 
            {
                foreach (var item in collection)
                    count++;
            }

            return count;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
