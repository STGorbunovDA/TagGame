using System;
using System.Globalization;
using System.Windows.Data;

namespace TagGameWPF
{
    public class IntPositiveToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int n = (int)value;
            return Math.Sign(n);
        }

        /// <summary>
        /// Из контролов в данные
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
