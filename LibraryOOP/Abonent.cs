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
		public ReadOnlyCollection<PhoneNumber> PhoneNumbers;

		internal Abonent(string name, string surname, List<PhoneNumber> phones, DateTime? date = null, string residence = null)
		{
			IsCorrect(name, surname);

			_phoneNumbers = phones;
			PhoneNumbers = phones.AsReadOnly();
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

		public List<PhoneNumber> GetNumbers(PhoneType type)
		{
			List<PhoneNumber> phones = new();

			foreach (var item in PhoneNumbers)
			{
				if (item.Type == type)
					phones.Add(item);
			}
			return phones;
		}

		public bool DeletePhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			if (PhoneNumbers.Contains(phone))
			{
				_phoneNumbers.Remove(phone);
				return true;
			}
			return false;
		}

		public bool AddPhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			if (!PhoneNumbers.Contains(phone))
			{
				_phoneNumbers.Add(phone);
				return true;
			}
			return false;
		}

		public bool IsContainsPhone(PhoneNumber phone)
		{
			if (phone == null) return false;

			foreach (var item in PhoneNumbers)
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
				return Equals(abonent.Name, Name) && Equals(abonent.Surname, Surname)
					&& Equals(abonent.Residence, Residence) && Equals(abonent.DateOfBirth, DateOfBirth);
			return false;
		}

		public override string ToString()
		{
			string returnResult = $"{Name}\n{Surname}";

			if (!(DateOfBirth is null)) returnResult += $"\n{DateOfBirth}";
			if (!(Residence is null)) returnResult += $"\n{Residence}";

			foreach (var item in PhoneNumbers)
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
