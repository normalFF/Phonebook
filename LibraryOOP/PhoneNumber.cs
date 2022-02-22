using System;

namespace LibraryOOP
{
	public enum PhoneType { Рабочий, Домашний, Личный}

	public struct PhoneNumber
	{
		private PhoneType _type;
		private string _phone;

		public PhoneType Type
		{
			get => _type;
		}
		public string Phone
		{
			get => _phone;
		}

		public PhoneNumber(string phone, PhoneType type)
		{
			_type = type;
			_phone = phone;
		}

		public override bool Equals(object obj)
		{
			if (obj is PhoneNumber phone)
				return phone.Type == Type && string.Equals(phone.Phone, Phone);
			return false;
		}

		public static bool operator ==(PhoneNumber phone1, PhoneNumber phone2)
		{
			return phone1.Equals(phone2);
		}

		public static bool operator !=(PhoneNumber phone1, PhoneNumber phone2)
		{
			return !phone1.Equals(phone2);
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException("Метод не реализован");
		}

		public override string ToString()
		{
			return $"{Type}\n{Phone}";
		}
	}
}
