using System;

namespace LibraryOOP
{
	public enum PhoneType { Рабочий, Домашний, Личный }

	public class PhoneNumber
	{
		public PhoneType Type { get; private set; }
		public string Phone { get; private set; }

		internal PhoneNumber(string phone, PhoneType type)
		{
			if (!IsCorrectPhone(phone)) throw new ArgumentException($"{phone} содержит некорректное значение");
			Type = type;
			Phone = phone;
		}

		public static bool IsCorrectPhone(string phone)
		{
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj is PhoneNumber phone)
				return phone.Type == Type && string.Equals(phone.Phone, Phone);
			return false;
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
