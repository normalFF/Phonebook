using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraryOOP
{
	public class Abonent
	{
		private List<PhoneNumber> _phoneNumbers { get; set; }

		public string Name { get; private set; }
		public string Surname { get; private set; }
		public DateTime? DateOfBirth { get; private set; }
		public string Residence { get; private set; }
		public IEnumerable<PhoneNumber> PhoneNumbers { get => _phoneNumbers; }

		internal Abonent(string name, string surname, List<PhoneNumber> phones, DateTime? date = null, string residence = null)
		{
			IsCorrect(name, surname);

			_phoneNumbers = phones;
			Name = name;
			Surname = surname;
			DateOfBirth = date;
			Residence = residence;
		}

		private static void IsCorrect(string name, string surname)
		{
			if (name == null) throw new ArgumentNullException($"{nameof(name)} не может быть {name}");
			if (surname == null) throw new ArgumentNullException($"{nameof(surname)} не может быть {surname}");
		}

		public IEnumerable<PhoneNumber> GetNumbers(PhoneType type)
		{
			List<PhoneNumber> phones = new();

			foreach (var item in _phoneNumbers)
			{
				if (item.Type == type)
					phones.Add(item);
			}
			return phones;
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

		public bool IsContainsPhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			foreach (var item in _phoneNumbers)
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
			string returnResult = $"{Name}\n{Surname}";

			if (!(DateOfBirth is null)) returnResult += $"\n{DateOfBirth}";
			if (!(Residence is null)) returnResult += $"\n{Residence}";

			foreach (var item in _phoneNumbers)
			{
				returnResult += $"\n{item.Type} {item.Phone}";
			}

			return returnResult;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
