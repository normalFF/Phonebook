using System;
using System.Collections.Generic;

namespace LibraryOOP
{
	public class AbonentsGroup
	{
		private string _name;
		private List<Abonent> _abonents;

		public string Name
		{
			get => _name;
			private set => _name = value;
		}

		public List<Abonent> Abonents
		{
			get => _abonents;
			private set => _abonents = value;
		}

		public AbonentsGroup(string name, List<Abonent> abonents)
		{
			if (!IsNull(name, abonents)) throw new ArgumentNullException("Некорректные данные");
			Name = name;
			Abonents = abonents;
		}

		public AbonentsGroup(string name, Abonent abonent)
		{
			if (!IsNull(name, abonent)) throw new ArgumentNullException("Некорректные данные");
			Name = name;
			Abonents = new() { abonent };
		}

		private static bool IsNull(string name, object abonents)
		{
			if (name is null || abonents is null) return false;
			return true;
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

		public bool RemoveAbonent(Abonent abonent)
		{
			if (Abonents.Contains(abonent))
			{
				Abonents.Remove(abonent);
				return true;
			}
			return false;
		}
	}
}
