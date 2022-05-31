using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using LibraryOOP;

namespace PhoneBookWPF.Infrastructure.Converter
{
	internal class PhoneNumberConvertor : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is List<PhoneNumber> numbers)
			{
				var returnResult = string.Empty;
				var dicktionaryPhones = numbers.GroupBy(t => t.Type);

				foreach (var item in dicktionaryPhones)
				{
					returnResult += $"Телефоны: {item.Key}\n";
					foreach (var phones in item)
					{
						returnResult += $"	{phones.Phone}\n";
					}
				}

				return returnResult.Trim('\n');
			}

			return "Отсутствует";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
