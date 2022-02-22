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

		public bool CheckNameGroup(string name)
		{
			if (name is null) return false;
			foreach (var item in AbonentsGroups)
			{
				if (item.Name.Equals(name)) return false;
			}
			return true;
		}
		
		public bool CreateAbonentsGroup(string name, Abonent abonent)
		{
			if (CheckIsNull(name, abonent)) return false;
			var group = new AbonentsGroup(name, abonent);
			AbonentsGroups.Add(group);
			return true;
		}

		public bool CreateAbonentsGroup(string name, List<Abonent> abonents)
		{
			if (CheckIsNull(name, abonents)) return false;
			var group = new AbonentsGroup(name, abonents);
			AbonentsGroups.Add(group);
			return true;
		}

		public bool AddAbonentGroup(AbonentsGroup group, Abonent abonent)
		{
			if (group is null || abonent is null) return false;
			return group.AddAbonent(abonent);
		}

		public bool AddAbonentsGroup(AbonentsGroup group, List<Abonent> abonents)
		{
			if (group is null || abonents is null) return false;

			foreach (var item in abonents)
			{
				group.AddAbonent(item);
			}
			return true;
		}

		public bool RemoveGroup(AbonentsGroup group)
		{
			if (AbonentsGroups.Contains(group))
			{
				AbonentsGroups.Remove(group);
				return true;
			}
			return false;
		}

		private static bool CheckIsNull(string name, object abonent)
		{
			return name is null || abonent is null;
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
