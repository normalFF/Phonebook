using LibraryOOP;
using System.Collections.Generic;

namespace PhoneBookWPF.Models
{
	internal class MainViewModel : BaseViewModel
	{
		private PhoneType _main, _home, _work;
		private PhoneBook _phoneBook;
		private List<Abonent> _abonents;
		private List<AbonentsGroup> _abonentsGroups;
		private Abonent _selectedAbonent;
		private AbonentsGroup _selectedAbonentsGroup;

		public List<Abonent> Abonents
		{
			get => _abonents;
			set => Set(ref _abonents, value);
		}
		public List<AbonentsGroup> AbonentsGroups
		{
			get => _abonentsGroups;
			set => Set(ref _abonentsGroups, value);
		}
		public Abonent SelectedAbonent
		{
			get => _selectedAbonent;
			set => Set(ref _selectedAbonent, value);
		}
		public AbonentsGroup SelectedAbonentsGroup
		{
			get => _selectedAbonentsGroup;
			set => Set(ref _selectedAbonentsGroup, value);
		}
		public PhoneType Main => _main;
		public PhoneType Home => _home;
		public PhoneType Work => _work;

		public MainViewModel()
		{
			_phoneBook = PhoneBook.GetPhoneBook();
			GetData.GetListAbonents(_phoneBook, 20);
			Abonents = _phoneBook.Abonents;
			AbonentsGroups = _phoneBook.AbonentsGroups;
			_main = PhoneType.Личный;
			_home = PhoneType.Домашний;
			_work = PhoneType.Рабочий;
		}
	}
}
