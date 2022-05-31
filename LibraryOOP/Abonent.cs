using System;
using System.Collections.Generic;

namespace LibraryOOP
{
	public class Abonent
	{
		private List<string> _groups { get; set; }
		private List<PhoneNumber> _phoneNumbers { get; set; }

		public string Name { get; private set; }
		public string Surname { get; private set; }
		public DateTime? DateOfBirth { get; private set; }
		public string Residence { get; private set; }
		public IEnumerable<string> Groups => _groups == null ? null : _groups;
		public IEnumerable<PhoneNumber> PhoneNumbers => _phoneNumbers;

		internal Abonent(string name, string surname, List<PhoneNumber> phones, DateTime? date = null, string residence = null)
		{
			_phoneNumbers = phones;
			_groups = new();
			Name = name;
			Surname = surname;
			DateOfBirth = date;
			Residence = residence;
		}

		internal Abonent(SerializedModelAbonent serializedData, List<PhoneNumber> phones)
		{
			Name = serializedData.Name;
			Surname = serializedData.Surname;
			DateOfBirth = serializedData.DateOfBirth == null ? null : (DateTime?)Convert.ToDateTime(serializedData.DateOfBirth);
			Residence = serializedData.Residence;
			_phoneNumbers = phones;
			_groups = new();
		}

		public static bool IsCorrect(string name, string surname, List<PhoneNumber> phones, bool getInfo = false, DateTime? date = null)
		{
			if (name == null)
			{
				if (getInfo)
				{
					throw new ArgumentNullException($"{nameof(name)} не может быть {name}");
				}
				return false;
			}
			if (surname == null)
			{
				if (getInfo)
				{
					throw new ArgumentNullException($"{nameof(surname)} не может быть {surname}");
				}
				return false;
			}
			if (date != null && DateTime.Compare((DateTime)date, DateTime.Now) > 0)
			{
				if (getInfo)
				{
					throw new ArgumentOutOfRangeException($"{nameof(date)} не может быть позже чем {DateTime.Now}");
				}
				return false;
			}
			if (phones == null)
			{
				if (getInfo)
				{
					throw new ArgumentNullException($"{nameof(phones)} должен содержать хотябы один номер телефона");
				}
				return false;
			}
			return true;
		}

		internal bool DeletePhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			if (_phoneNumbers.Contains(phone))
			{
				_phoneNumbers.Remove(phone);
				return true;
			}
			return false;
		}

		internal bool AddPhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			if (!_phoneNumbers.Contains(phone))
			{
				_phoneNumbers.Add(phone);
				return true;
			}
			return false;
		}

		internal bool AddGroup(string group)
		{
			if (string.IsNullOrEmpty(group) || _groups.Contains(group))
			{
				return false;
			}
			_groups.Add(group);
			return true;
		}

		internal bool DeleteGroup(string group)
		{
			if (string.IsNullOrEmpty(group) || !_groups.Contains(group))
			{
				return false;
			}
			_groups.Remove(group);
			return true;
		}

		public bool IsContainsPhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			foreach (PhoneNumber item in _phoneNumbers)
			{
				if (item.Equals(phone))
				{
					return true;
				}
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is Abonent abonent)
				return string.Equals(abonent.Name, Name, StringComparison.OrdinalIgnoreCase) &&
					string.Equals(abonent.Surname, Surname, StringComparison.OrdinalIgnoreCase) &&
					string.Equals(abonent.Residence, Residence, StringComparison.OrdinalIgnoreCase) &&
					Equals(abonent.DateOfBirth, DateOfBirth);
			return false;
		}

		public override string ToString()
		{
			string returnResult = $"{Name}\r\n{Surname}";

			if (!(DateOfBirth is null)) returnResult += $"\r\n{DateOfBirth?.ToString("dd.MM.yyyy")}";
			if (!(Residence is null)) returnResult += $"\r\n{Residence}";

			foreach (PhoneNumber item in _phoneNumbers)
			{
				returnResult += $"\r\n{item.Type} {item.Phone}";
			}

			return returnResult;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
