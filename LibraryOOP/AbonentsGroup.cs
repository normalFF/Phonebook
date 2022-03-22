using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LibraryOOP
{
	public class AbonentsGroup
	{
		private List<Abonent> _abonents { get; set; }

		public string Name { get; private set; }
		public ReadOnlyCollection<Abonent> Abonents { get; }

		internal AbonentsGroup(string name, List<Abonent> abonents)
		{
			IsNull(name, abonents);
			Name = name;
			_abonents = abonents;
			Abonents = _abonents.AsReadOnly();
		}

		internal AbonentsGroup(string name, Abonent abonent)
		{
			IsNull(name, Abonents);
			Name = name;
			_abonents = new() { abonent };
			Abonents = _abonents.AsReadOnly();
		}

		private static void IsNull(string name, object abonents)
		{
			if (name == null) throw new ArgumentNullException($"{nameof(name)} является {name}");
			if (abonents == null) throw new ArgumentNullException($"{nameof(abonents)} является {abonents}");
			if (abonents is List<Abonent> abonent)
			{
				if (abonent.Count == 0) throw new ArgumentException($"{abonent} не может содержать {abonent.Count} элементов");
				foreach (var item in abonent)
				{
					if (item == null) throw new AggregateException($"Элемент {nameof(item)} в {abonent} не может быть {item}");
				}
			}
		}

		public bool AddAbonent(Abonent abonent)
		{
			if (abonent == null) return false;

			if (!_abonents.Contains(abonent))
			{
				_abonents.Add(abonent);
				return true;
			}
			return false;
		}

		public bool RemoveAbonent(Abonent abonent)
		{
			if (abonent == null) return false;

			if (_abonents.Contains(abonent))
			{
				_abonents.Remove(abonent);
				return true;
			}
			return false;
		}
	}
}