using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using LibraryOOP;

namespace PhoneBookWPF.Infrastructure.Converter
{
	internal class PhoneNumberConvertorMain : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is List<PhoneNumber> numbers)
			{
				string returnString = "";
				foreach (var item in numbers)
				{
					if ((int)item.Type == 2)
					{
						if (returnString.Length == 0)
						{
							returnString += $"{item.Phone}";
						}
						else
						{
							returnString += $"\n{item.Phone}";
						}
					}
				}
				if (returnString.Length == 0) return "Отсутствует";
				return returnString;
			}

			return "Отсутствует";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
