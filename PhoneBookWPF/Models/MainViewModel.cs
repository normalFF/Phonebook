using System;
using LibraryOOP;
using System.Collections.Generic;

namespace PhoneBookWPF.Models
{
	internal class MainViewModel : BaseViewModel
	{
		private PhoneBook _phoneBook;
		private List<Abonent> _abonents;
		private List<AbonentsGroup> _abonentsGroups;
		private Abonent _selectedAbonent;
		private AbonentsGroup _selectedAbonentsGroup;

		private string _nameCreate;
		private string _surnameCreate;
		private string _residentCreate;
		private DateTime? _dateTimeCreate;
		private List<PhoneNumber> _phonesMainCreate;
		private string _phoneMainAdd;
		private PhoneNumber _phoneMainSelect;
		private List<PhoneNumber> _phonesHomeCreate;
		private string _phoneHomeAdd;
		private PhoneNumber _phoneHomeSelect;
		private List<PhoneNumber> _phonesWorkCreate;
		private string _phoneWorkAdd;
		private PhoneNumber _phoneWorkSelect;

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

		public string NameCreate
		{
			get => _nameCreate;
			set => Set(ref _nameCreate, value);
		}
		public string SurnameCreate
		{
			get => _surnameCreate;
			set => Set(ref _surnameCreate, value);
		}
		public string ResidentCreate
		{
			get => _residentCreate;
			set => Set(ref _residentCreate, value);
		}
		public DateTime? DateTimeCreate
		{
			get => _dateTimeCreate;
			set => Set(ref _dateTimeCreate, value);
		}
		public List<PhoneNumber> PhonesMainCreate
		{
			get => _phonesMainCreate;
			set => Set(ref _phonesMainCreate, value);
		}
		public string PhoneMainAdd
		{
			get => _phoneMainAdd;
			set => Set(ref _phoneMainAdd, value);
		}
		public PhoneNumber PhoneMainSelect
		{
			get => _phoneMainSelect;
			set => Set(ref _phoneMainSelect, value);
		}
		public List<PhoneNumber> PhonesHomeCreate
		{
			get => _phonesHomeCreate;
			set => Set(ref _phonesHomeCreate, value);
		}
		public string PhoneHomeAdd
		{
			get => _phoneHomeAdd;
			set => Set(ref _phoneHomeAdd, value);
		}
		public PhoneNumber PhoneHomeSelect
		{
			get => _phoneHomeSelect;
			set => Set(ref _phoneHomeSelect, value);
		}
		public List<PhoneNumber> PhonesWorkCreate
		{
			get => _phonesWorkCreate;
			set => Set(ref _phonesWorkCreate, value);
		}
		public string PhoneWorkAdd
		{
			get => _phoneWorkAdd;
			set => Set(ref _phoneWorkAdd, value);
		}
		public PhoneNumber PhoneWorkSelect
		{
			get => _phoneWorkSelect;
			set => Set(ref _phoneWorkSelect, value);
		}

		public MainViewModel()
		{
			_phoneBook = PhoneBook.GetPhoneBook();
			GetData.GetListAbonents(_phoneBook, 20);
			Abonents = _phoneBook.Abonents;
			AbonentsGroups = _phoneBook.AbonentsGroups;
		}


	}
}
