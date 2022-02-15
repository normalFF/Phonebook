using System;

namespace LibraryOOP
{
	public enum PhoneType { Рабочий, Домашний, Личный}

	public struct PhoneNumber
	{
		private PhoneType _type;
		private string _phone;

		public PhoneType Type => _type;
		public string Phone => _phone;

		public PhoneNumber(string phone, PhoneType type)
		{
			_phone = phone;
			_type = type;
		}

		public bool CheckTypePhone(PhoneType type)
		{
			return Equals(Type, type);
		}

		public static bool IsCorrectNumber(string number)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			if (obj is PhoneNumber phone)
				return phone.CheckTypePhone(Type) && string.Equals(phone.Phone, Phone);
			return false;
		}

		public override int GetHashCode()
		{
			int result = 0;
			for (int i = 0; i < Phone.Length; i++)
			{
				result += (int)Math.Pow(Convert.ToInt32(Phone[i]), i);
			}

			return result * (int)Type;
		}
	}
}
