using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PhoneBookWPF.Infrastructure.Converter
{
	internal class VisibleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility visible)
			{
				if (visible == Visibility.Visible) return Visibility.Collapsed;
				else return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
