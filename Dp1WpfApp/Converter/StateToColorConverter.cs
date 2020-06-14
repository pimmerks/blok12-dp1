using DP1.Library;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Dp1WpfApp.Converter
{
    public class StateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is State state)
            {
                if (state.LogicState)
                {
                    return new SolidColorBrush(Colors.Green);
                } else
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }

            return new SolidColorBrush(Colors.Black); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
