using System;
using System.Collections.Generic;

namespace LibraryOOP
{
	public class Abonent
	{
		private readonly string _name;
		private string _surname;
		private DateTime? _dateOfBirth;
		private string _residence;
		private List<PhoneNumber> _phoneNumbers;

		public string Name { get => _name; }
		public string Surname { get => _surname; }
		public DateTime? DateOfBirth { get => _dateOfBirth; }
		public string Residence { get => _residence; }
		public List<PhoneNumber> PhoneNumbers { get => _phoneNumbers;}

		public Abonent(string name, string surname, PhoneNumber phone, DateTime? date = null, string residence = null)
		{
			_phoneNumbers = new List<PhoneNumber>() { phone };
			_name = name;
			_surname = surname;
			_dateOfBirth = date;
			_residence = residence;
		}

		public List<PhoneNumber> GetNumbers(PhoneType type)
		{
			List<PhoneNumber> phones = new();

			foreach (var item in _phoneNumbers)
			{
				if (item.CheckTypePhone(type))
					phones.Add(item);
			}

			return phones;
		}

		public bool DeletePhone(PhoneNumber phone)
		{
			if (_phoneNumbers.Contains(phone))
			{
				_phoneNumbers.Remove(phone);
				return true;
			}
			return false;
		}

		public bool AddPhone(PhoneNumber phone)
		{
			if (!_phoneNumbers.Contains(phone))
			{
				_phoneNumbers.Add(phone);
				return true;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is Abonent abonent)
				return Equals(abonent.Name, Name) && Equals(abonent.Surname, Surname)
					&& Equals(abonent.Residence, Residence) && Equals(abonent.DateOfBirth, DateOfBirth);
			return false;
		}

		public override string ToString()
		{
			string returnResult = $"{Name}\n{Surname}";

			if (!(DateOfBirth is null)) returnResult += $"\n{DateOfBirth}";
			if (!(Residence is null)) returnResult += $"\n{Residence}";

			foreach (var item in _phoneNumbers)
			{
				returnResult += $"{item.Type} {item.Phone}";
			}

			return returnResult;
		}
	}
}
