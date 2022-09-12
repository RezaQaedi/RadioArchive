using System;
using System.Globalization;
using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// converter takes boolin and return <see cref="Visibility"/>
    /// Where Flase is <see cref="Visibility.Collapsed"/>
    /// </summary>
    public class HasReplayShowToVisibility : BaseMultiValueConverter<HasReplayShowToVisibility>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var hasReplay = (PodcastTime)values[0]; 
            var time = (PodcastTime)values[1];
            var visiblity = Visibility.Collapsed;

            if(time == PodcastTime.Evening || time == PodcastTime.Both)
                if(hasReplay == PodcastTime.Both || hasReplay == PodcastTime.Evening)
                    visiblity  = Visibility.Visible;
            else if (time == PodcastTime.Morning || time == PodcastTime.Both)
                if(hasReplay == PodcastTime.Both || hasReplay == PodcastTime.Morning)
                    visiblity= Visibility.Visible;
            else if (time == PodcastTime.none)
                if (hasReplay == PodcastTime.Both)
                    visiblity = Visibility.Visible;


            return visiblity;
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
