using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Religious_Composition_Program_Ver01
{
    public class StringToDescBG : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string gridText = (string)values[0];
            string selectedDesc = (string)values[1];

            if (gridText == selectedDesc)
                return (Brush)(new BrushConverter().ConvertFromString("#D2FF6A")); 
            else
                return (Brush)(new BrushConverter().ConvertFromString("#F7FFF0"));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToBG : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string gridText = (string)values[0];
            string selectedDesc = (string)values[1];

            if (gridText == selectedDesc)
                return (Brush)(new BrushConverter().ConvertFromString("#7700B8"));
            else
                return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
