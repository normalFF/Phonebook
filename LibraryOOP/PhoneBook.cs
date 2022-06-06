using System;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using System.Globalization;

namespace LibraryOOP
{
	public class PhoneBook
	{
		private static PhoneBook _phoneBook;
		private List<Abonent> _abonents { get; set; }
		private List<string> _abonentsGroups { get; set; }
		private List<string> _phoneType { get; set; }
		private bool _saved;


		public IEnumerable<Abonent> Abonents => _abonents;
		public IEnumerable<string> AbonentsGroup => _abonentsGroups;
		public IEnumerable<string> PhoneType => _phoneType;
		public int AbonentCount { get => _abonents.Count; }
		public int AbonentsGroupCount { get => _abonentsGroups.Count; }

		private PhoneBook()
		{
			_abonents = new();
			_abonentsGroups = new();
			_phoneType = new();
			_saved = true;
		}

		public static PhoneBook GetPhoneBook()
		{
			if (_phoneBook is null)
			{
				lock (new object())
				{
					if (_phoneBook is null)
					{
						_phoneBook = new PhoneBook();
					}
				}
			}
			return _phoneBook;
		}

		public static void DeletePhoneBook()
		{
			_phoneBook = null;
		}

		public bool IsSaved()
		{
			return _saved;
		}

		public void LoadData(string fileWay)
		{
			if (Path.GetExtension(fileWay) != ".json")
			{
				throw new NotSupportedException($"Файл {fileWay} не соответствует допустимому формату (.json)");
			}

			SerializedModelAbonent[] loadData;
			string readResult;

			using (StreamReader file = new(fileWay))
			{
				readResult = file.ReadToEnd();
			}

			if (string.IsNullOrEmpty(readResult))
			{
				return;
			}

			loadData = (SerializedModelAbonent[])JsonSerializer.Deserialize(
				readResult,
				typeof(SerializedModelAbonent[]),
				new JsonSerializerOptions()
				{
					WriteIndented = true,
					AllowTrailingCommas = true,
					Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				}
			);

			List<Abonent> abonents =
				loadData.Where(i => Abonent.IsCorrect(
								i.Name,
								i.Surname,
								i.Phones.Where(p => PhoneNumber.IsCorrectPhone(p.Phone, p.Type, true))
										.Select(p => PhoneNumber.CreatePhoneNumber(p)).ToList(),
								true,
								i.DateOfBirth == null ? null : Convert.ToDateTime(i.DateOfBirth, DateTimeFormatInfo.CurrentInfo)))
						.Select(i => new Abonent(
								i,
								i.Phones.Select(p => PhoneNumber.CreatePhoneNumber(p)).ToList()))
						.ToList();


			List<IEnumerable<string>> groups = abonents.Where(t => t.Groups.Count() > 0).Select(t => t.Groups).ToList();
			List<IEnumerable<PhoneNumber>> phoneTypes = abonents.Where(t => t.PhoneNumbers.Count() > 0).Select(t => t.PhoneNumbers).ToList();

			List<string> abonentsGroups = groups.Count > 0 ? groups[0].ToList() : new List<string>();
			_phoneType = new();

			foreach (IEnumerable<string> item in groups)
			{
				abonentsGroups = abonentsGroups.Union(item).ToList();
			}
			foreach (IEnumerable<PhoneNumber> item in phoneTypes)
			{
				foreach (PhoneNumber phone in item)
				{
					if (!_phoneType.Contains(phone.Type))
					{
						_phoneType.Add(phone.Type);
					}
				}
			}

			_abonentsGroups = abonentsGroups;
			_abonents = abonents;
		}

		public void SaveData(string fileWay)
		{
			if (Path.GetExtension(fileWay) != ".json")
			{
				throw new NotSupportedException($"Файл {fileWay} не соответствует допустимому формату (.json)");
			}

			SerializedModelAbonent[] serializeObject = Abonents.Select(i => new SerializedModelAbonent
			{
				Name = i.Name,
				Surname = i.Surname,
				DateOfBirth = i.DateOfBirth?.ToString("dd.MM.yyyy"),
				Residence = i.Residence,
				Phones = i.PhoneNumbers.Select(i => new SerializedModelPhone() 
				{
					Type = i.Type,
					Phone = i.Phone
				}).ToArray(),
				Groups = i.Groups?.ToArray(),
			}).ToArray();

			string serializeResult = JsonSerializer.Serialize(
					serializeObject,
					typeof(SerializedModelAbonent[]),
					new JsonSerializerOptions()
					{
						WriteIndented = true,
						AllowTrailingCommas = true,
						Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
					}
				);

			using (StreamWriter file = new(fileWay))
			{
				file.Write(serializeResult);
			}

			_saved = true;
		}

		public static PhoneNumber CreatePhoneNumber(string phone, string phoneType)
		{
			return PhoneNumber.CreatePhoneNumber(phone, phoneType);
		}


		public bool AddAbonent(string name, string surname, List<PhoneNumber> phones, DateTime? date = null, string residence = null)
		{
			foreach (PhoneNumber item in phones)
			{
				if (item == null)
				{
					return false;
				}
			}

			Abonent newAbonent = new(name, surname, phones, date, residence);

			if (!_abonents.Contains(newAbonent))
			{
				_abonents.Add(newAbonent);

				foreach (PhoneNumber item in newAbonent.PhoneNumbers)
				{
					if (!_phoneType.Contains(item.Type))
					{
						_phoneType.Add(item.Type);
					}
				}

				_saved = false;
				return true;
			}
			return false;
		}

		public bool AddAbonent(string name, string surname, PhoneNumber phone, DateTime? date = null, string residence = null)
		{
			if (phone == null)
			{
				return false;
			}
			if (AddAbonent(name, surname, new List<PhoneNumber>() { phone }, date, residence))
			{
				_saved = false;
				return true;
			}
			return false;
		}

		public bool AddAbonentsGroup(string group, Abonent abonent)
		{
			if (abonent.AddGroup(group))
			{
				_saved = false;
				if (!_abonentsGroups.Contains(group))
				{
					_abonentsGroups.Add(group);
				}
				return true;
			}
			return false;
		}


		public bool AddAbonentPhone(Abonent abonent, PhoneNumber phone)
		{
			if (abonent != null)
			{
				if (abonent.AddPhone(phone))
				{
					_saved = false;
					if (!_phoneType.Contains(phone.Type))
					{
						_phoneType.Add(phone.Type);
					}
					return true;
				}
			}
			return false;
		}

		public bool RemoveAbonentPhone(Abonent abonent, PhoneNumber phone)
		{
			if (abonent != null)
			{
				if (abonent.DeletePhone(phone))
				{
					_saved = false;
					return true;
				}
			}
			return false;
		}

		public bool RemoveAbonent(Abonent abonent)
		{
			if (_abonents.Contains(abonent))
			{
				foreach (string group in abonent.Groups)
				{
					int count = 0;
					foreach (Abonent item in _abonents)
					{
						if (item.Groups.Contains(group))
						{
							count++;
						}
					}
					if (count == 1)
					{
						_abonentsGroups.Remove(group);
					}
				}

				_abonents.Remove(abonent);
				_saved = false;
				return true;
			}
			return false;
		}

		public bool RemoveGroup(string group)
		{
			if (_abonentsGroups.Contains(group))
			{
				foreach (Abonent item in _abonents)
				{
					item.DeleteGroup(group);
				}
				_abonentsGroups.Remove(group);
				_saved = false;
				return true;
			}
			return false;
		}


		public bool CheckNameGroup(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			foreach (string item in _abonentsGroups)
			{
				if (item.Equals(name, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}
}
