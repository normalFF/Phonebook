using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraryOOP
{
	public class PhoneBook
	{
		private static PhoneBook _phoneBook;
		private List<Abonent> _abonents { get; set; }
		private List<AbonentsGroup> _abonentsGroups { get; set; }
		
		public IEnumerable<Abonent> Abonents { get => _abonents; }
		public IEnumerable<AbonentsGroup> AbonentsGroup { get => _abonentsGroups; }

		public int AbonentCount { get => _abonents.Count; }
		public int AbonentsGroupCount { get => _abonentsGroups.Count; }

		private PhoneBook()
		{
			_abonents = new();
			_abonentsGroups = new();
		}

		public static PhoneBook GetPhoneBook()
		{
			if (_phoneBook is null)
			{
				lock (new object())
				{
					if (_phoneBook is null)
						_phoneBook = new PhoneBook();
				}
			}
			return _phoneBook;
		}

		public static PhoneNumber CreatePhoneNumber(string phone, PhoneType phoneType)
		{
			return PhoneNumber.CreatePhoneNumber(phone, phoneType);
		}

		private static bool CheckIsNull(string name, object abonent)
		{
			return name is null || abonent is null;
		}


		public bool AddAbonent(string name, string surname, List<PhoneNumber> phones, DateTime? date = null, string residence = null)
		{
			foreach (var item in phones)
			{
				if (item == null) return false;
			}

			Abonent newAbonent = new(name, surname, phones, date, residence);
			
			if (!_abonents.Contains(newAbonent))
			{
				_abonents.Add(newAbonent);
				return true;
			}
			return false;
		}

		public bool AddAbonent(string name, string surname, PhoneNumber phone, DateTime? date = null, string residence = null)
		{
			if (phone == null) return false;
			return AddAbonent(name, surname, new List<PhoneNumber>() { phone }, date, residence);
		}

		public bool AddAbonentsGroup(AbonentsGroup group, Abonent abonent)
		{
			return AddAbonentsGroup(group, new List<Abonent>() { abonent });
		}

		public bool AddAbonentsGroup(AbonentsGroup group, List<Abonent> abonents)
		{
			if (group == null || abonents == null) return false;

			foreach (var item in abonents)
			{
				group.AddAbonent(item);
			}
			return true;
		}


		public bool CreateAbonentsGroup(string name, Abonent abonent)
		{
			return CreateAbonentsGroup(name, new List<Abonent>() { abonent });
		}

		public bool CreateAbonentsGroup(string name, List<Abonent> abonents)
		{
			if (CheckIsNull(name, abonents)) return false;
			var group = new AbonentsGroup(name, abonents);
			_abonentsGroups.Add(group);
			return true;
		}


		public bool AddAbonentPhone(Abonent abonent, PhoneNumber phone)
		{
			if (phone == null) return false;
			if (abonent.IsContainsPhone(phone)) return false;
			else
			{
				abonent.AddPhone(phone);
				return true;
			}
		}

		public bool RemoveAbonentPhone(Abonent abonent, PhoneNumber phone)
		{
			if (abonent.IsContainsPhone(phone)) return false;
			else
			{
				abonent.DeletePhone(phone);
				return true;
			}
		}

		public bool RemoveAbonent(Abonent abonent)
		{
			if (_abonents.Contains(abonent))
			{
				foreach (var item in _abonentsGroups)
				{
					item.RemoveAbonent(abonent);
				}
				_abonents.Remove(abonent);

				return true;
			}
			return false;
		}

		public bool RemoveGroup(AbonentsGroup group)
		{
			if (_abonentsGroups.Contains(group))
			{
				_abonentsGroups.Remove(group);
				return true;
			}
			return false;
		}

		
		public bool CheckNameGroup(string name)
		{
			if (name is null) return false;
			foreach (var item in _abonentsGroups)
			{
				if (item.Name.Equals(name)) return false;
			}
			return true;
		}
	}
}
