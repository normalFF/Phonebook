using LibraryOOP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PhoneBookWPF.Infrastructure.Converter
{
	internal class IEnumerableAbonentConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var collection = value as IEnumerable<Abonent>;
			return collection.ToList();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
