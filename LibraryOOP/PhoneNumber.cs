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
				return phone.Type == Type && string.Equals(phone.Phone, this.Phone);
			return false;
		}

		public override string ToString()
		{
			return $"{Type}\n{Phone}";
		}
	}
}
