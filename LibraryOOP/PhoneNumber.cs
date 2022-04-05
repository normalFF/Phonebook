using System;

namespace LibraryOOP
{
	public enum PhoneType { Рабочий, Домашний, Личный }

	public class PhoneNumber
	{
		private static readonly char[]
			_numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' },
			_serviceSymbols = new char[] { '-', '(', ')', ' ', '.', '+', 'x' };

		public PhoneType Type { get; private set; }
		public string Phone { get; private set; }

		private PhoneNumber(string phone, PhoneType type)
		{
			Type = type;
			Phone = phone;
		}

		internal static PhoneNumber CreatePhoneNumber(string phone, PhoneType type)
		{
			if (!IsCorrectPhone(phone)) return null;
			return new PhoneNumber(phone, type);
		}

		public static bool IsCorrectPhone(string phone)
		{
			var result = phone.TrimStart(new char[] { 'x', '+', '-', '*', '#', '0' });

			if (result.Length < 8) return false;

			var 
				count = result.Length;
			bool
				open = false,
				tire = false,
				zero = false,
				code = false;

			foreach (var item in result)
			{
				if (item != '0') zero = true;
				if ((item == 'x' || item == '+') && code) return false;
				if (item == 'x' || item == '+') code = true;

				if (item == '(' && !open) open = true;
				else if (item == '(' && open) return false;
				if (item == ')' && !open) return false;
				else if (item == ')' && open) open = false;

				if (item == '-' && !tire) tire = true;
				else if (item == '-' && tire) return false;
				else if (item != '-' && tire) tire = false;

				if (Check(item, _serviceSymbols))
				{
					count--;
				}
				else if (!Check(item, _numbers)) return false;

				if (count < 8) return false;
			}

			if (zero) return true;
			else return false;
		}

		private static bool Check(char item, char[] arrayChars)
		{
			foreach (var i in arrayChars)
			{
				if (item == i) return true;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is PhoneNumber phone)
				return phone.Type == Type && Equals(phone.Phone, Phone);
			return false;
		}

		private static bool Equals(string str1, string str2)
		{
			string Clear(string str)
			{
				var result = str.TrimStart(new char[] { '+', '-', '*', '#', '0' });

				foreach (var item in _serviceSymbols)
				{
					for (int i = result.Length - 1; i >= 0; i--)
					{
						if (result[i] == item) result = result.Remove(i, 1);
					}
				}
				return result;
			}

			return string.Equals(Clear(str1), Clear(str2));
		}

		public override int GetHashCode()
		{
			int result = 0;
			int count = 0;

			foreach (var item in Phone)
			{
				if (item != ')' && item != '(' && item != '-')
					result += (int)Math.Pow(Convert.ToInt32(item), 2 + count);
			}

			return result;
		}

		public override string ToString()
		{
			return $"{Type}\n{Phone}";
		}
	}
}
