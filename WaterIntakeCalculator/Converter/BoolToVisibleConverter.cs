using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WaterIntakeCalculator.Converter
{
    public class BoolToVisibleConverter : IValueConverter
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public BoolToVisibleConverter() { }
        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            if (bValue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }


        #endregion
    }
}