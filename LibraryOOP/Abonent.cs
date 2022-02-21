using System;
using System.Collections.Generic;

namespace LibraryOOP
{
	public class Abonent
	{
		private string _name;
		private string _surname;
		private DateTime? _dateOfBirth;
		private string _residence;
		private List<PhoneNumber> _phoneNumbers;

		public string Name 
		{ 
			get => _name;
			private set => _name = value;
		}
		public string Surname 
		{ 
			get => _surname;
			private set => _surname = value;
		}
		public DateTime? DateOfBirth 
		{ 
			get => _dateOfBirth;
			private set => _dateOfBirth = value;
		}
		public string Residence 
		{ 
			get => _residence;
			private set => _residence = value;
		}
		public List<PhoneNumber> PhoneNumbers 
		{ 
			get => _phoneNumbers;
			private set => _phoneNumbers = value;
		}

		public Abonent(string name, string surname, List<PhoneNumber> phones, DateTime? date = null, string residence = null)
		{
			if (IsNull(name, surname, phones)) throw new ArgumentNullException("Некорректные данные");

			PhoneNumbers = phones;
			Name = name;
			Surname = surname;
			DateOfBirth = date;
			Residence = residence;
		}

		public Abonent(string name, string surname, PhoneNumber phone, DateTime? date = null, string residence = null)
		{
			if (IsNull(name, surname, phone)) throw new ArgumentNullException("Некорректные данные");

			PhoneNumbers = new List<PhoneNumber>() { phone };
			Name = name;
			Surname = surname;
			DateOfBirth = date;
			Residence = residence;
		}

		private bool IsNull(string name, string surname, object phones)
		{
			if (name is null || surname is null || phones is null) return true;
			return false;
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
			if (PhoneNumbers.Contains(phone))
			{
				PhoneNumbers.Remove(phone);
				return true;
			}
			return false;
		}

		public bool AddPhone(PhoneNumber phone)
		{
			if (!PhoneNumbers.Contains(phone))
			{
				PhoneNumbers.Add(phone);
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
