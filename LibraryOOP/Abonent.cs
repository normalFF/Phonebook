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

		public string Name => _name;
		public string Surname => _surname;
		public DateTime? DateOfBirth => _dateOfBirth;
		public string Residence => _residence;

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
