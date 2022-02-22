using System.Collections.Generic;

namespace LibraryOOP
{
	public class PhoneBook
	{
		private List<Abonent> _abonents;
		private List<AbonentsGroup> _abonentsGroups;

		public List<Abonent> Abonents
		{
			get => _abonents;
			private set => _abonents = value;
		}
		public List<AbonentsGroup> AbonentsGroups
		{
			get => _abonentsGroups;
			private set => _abonentsGroups = value;
		}

		public bool AddAbonent(Abonent abonent)
		{
			if (!Abonents.Contains(abonent))
			{
				Abonents.Add(abonent);
				return true;
			}
			return false;
		}

		public bool AddAbonent(Abonent abonent, AbonentsGroup abonentsGroups)
		{
			AddAbonent(abonent);
			return abonentsGroups.AddAbonent(abonent);
		}

		public bool RemoveAbonent(Abonent abonent)
		{
			if (Abonents.Contains(abonent))
			{
				Abonents.Remove(abonent);
				foreach (var item in AbonentsGroups)
				{
					item.RemoveAbonent(abonent);
				}
				return true;
			}
			return false;
		}

		public bool CheckNumberInAbonents(PhoneNumber phone)
		{
			foreach (var item in Abonents)
			{
				if (item.IsContainsPhone(phone))
					return true;
			}
			return false;
		}
	}
}
