using LibraryOOP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PhoneBookWPF.Infrastructure.Converter
{
	internal class IEnumerableAbonentConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length == 1)
			{
				var collection = values[0] as IEnumerable<Abonent>;
				return collection.ToList();
			}
			if (values.Length == 2)
			{
				try
				{
					var collection = values[0] as IEnumerable<Abonent>;
					var param = values[1] as string;

					return collection.Where(t => t.Groups.Contains(param)).ToList();
				}
				catch (Exception)
				{
					return null;
				}
			}
			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
