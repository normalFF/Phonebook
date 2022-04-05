using System;
using LibraryOOP;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PhoneBookWPF.Command;
using System.Windows.Input;

namespace PhoneBookWPF.Models
{
	internal class MainViewModel : BaseViewModel
	{
		private readonly PhoneBook _phoneBook;
		private IEnumerable<Abonent> _abonents;
		private IEnumerable<AbonentsGroup> _abonentsGroups;
		private Abonent _selectedAbonent;
		private AbonentsGroup _selectedAbonentsGroup;

		private string _nameCreate;
		private string _surnameCreate;
		private string _residentCreate;
		private DateTime? _dateTimeCreate;
		private ObservableCollection<PhoneNumber> _phonesMainCreate;
		private string _phoneMainAdd;
		private PhoneNumber _phoneMainSelect;
		private ObservableCollection<PhoneNumber> _phonesHomeCreate;
		private string _phoneHomeAdd;
		private PhoneNumber _phoneHomeSelect;
		private ObservableCollection<PhoneNumber> _phonesWorkCreate;
		private string _phoneWorkAdd;
		private PhoneNumber _phoneWorkSelect;

		public IEnumerable<Abonent> Abonents
		{
			get => _abonents;
			set => Set(ref _abonents, value);
		}
		public IEnumerable<AbonentsGroup> AbonentsGroups
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
		public ObservableCollection<PhoneNumber> PhonesMainCreate
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
		public ObservableCollection<PhoneNumber> PhonesHomeCreate
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
		public ObservableCollection<PhoneNumber> PhonesWorkCreate
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

		public ICommand AddAbonentCommand { get; private set; }
		public ICommand ClearAbonentCommand { get; private set; }
		public ICommand AddPhoneMainCommand { get; private set; }
		public ICommand DeletePhoneMainCommand { get; private set; }
		public ICommand AddPhoneHomeCommand { get; private set; }
		public ICommand DeletePhoneHomeCommand { get; private set; }
		public ICommand AddPhoneWorkCommand { get; private set; }
		public ICommand DeletePhoneWorkCommand { get; private set; }

		public MainViewModel()
		{
			_phoneBook = PhoneBook.GetPhoneBook();
			GetData.GetListAbonents(_phoneBook, 20);
			Abonents = _phoneBook.Abonents;
			AbonentsGroups = _phoneBook.AbonentsGroup;

			CommandInitialization();
			ObjectInitialization();
		}

		private void ObjectInitialization()
		{
			PhonesMainCreate = new();
			PhonesHomeCreate = new();
			PhonesWorkCreate = new();
		}

		private void CommandInitialization()
		{
			AddAbonentCommand = new CommandBase(OnCreateAbonent, CanCreateAbonent);
			ClearAbonentCommand = new CommandBase(OnClearAbonent, CanClearAbonent);
			AddPhoneMainCommand = new CommandBase(OnAddPhoneMain, CanAddPhoneMain);
			DeletePhoneMainCommand = new CommandBase(OnDeletePhoneMain, CanDeletePhoneMain);
			AddPhoneHomeCommand = new CommandBase(OnAddPhoneHome, CanAddPhoneHome);
			DeletePhoneHomeCommand = new CommandBase(OnDeletePhoneHome, CanDeletePhoneHome);
			AddPhoneWorkCommand = new CommandBase(OnAddPhoneWork, CanAddPhoneWork);
			DeletePhoneWorkCommand = new CommandBase(OnDeletePhoneWork, CanDeletePhoneWork);
		}

		private bool CanCreateAbonent(object obj)
		{
			return NameCreate != null && NameCreate.Length > 0 && SurnameCreate != null && SurnameCreate.Length > 0 &&
				((PhonesMainCreate != null && PhonesMainCreate.Count > 0) || (PhonesHomeCreate != null && PhonesHomeCreate.Count > 0) || (PhonesWorkCreate != null && PhonesWorkCreate.Count > 0));
		}

		private void OnCreateAbonent(object obj)
		{

		}

		private bool CanClearAbonent(object obj) => true;

		private void OnClearAbonent(object obj)
		{
			NameCreate = null;
			SurnameCreate = null;
			ResidentCreate = null;
			DateTimeCreate = null;
			PhonesMainCreate = null;
			PhoneMainAdd = null;
			PhoneMainSelect = null;
			PhonesHomeCreate = null;
			PhoneHomeAdd = null;
			PhoneHomeSelect = null;
			PhonesWorkCreate = null;
			PhoneWorkAdd = null;
			PhoneWorkSelect = null;
		}

		private bool CanAddPhoneMain(object obj)
		{
			return PhoneMainAdd != null && PhoneMainAdd.Length > 0;
		}

		private void OnAddPhoneMain(object obj)
		{
			
		}

		private bool CanDeletePhoneMain(object obj)
		{
			return PhoneMainSelect != null;
		}

		private void OnDeletePhoneMain(object obj)
		{
			PhonesMainCreate.Remove(PhoneMainSelect);
			PhoneMainSelect = null;
		}

		private bool CanAddPhoneHome(object obj)
		{
			return PhoneHomeAdd != null && PhoneHomeAdd.Length > 0;
		}

		private void OnAddPhoneHome(object obj)
		{
			/*
			PhoneNumber phone = new(PhoneHomeAdd, PhoneType.Личный);
			PhonesHomeCreate.Add(phone);
			*/
		}

		private bool CanDeletePhoneHome(object obj)
		{
			return PhoneHomeSelect != null;
		}

		private void OnDeletePhoneHome(object obj)
		{
			PhonesHomeCreate.Remove(PhoneHomeSelect);
			PhoneHomeSelect = null;
		}

		private bool CanDeletePhoneWork(object arg)
		{
			return PhoneWorkSelect != null;
		}

		private void OnDeletePhoneWork(object obj)
		{
			PhonesWorkCreate.Remove(PhoneWorkSelect);
			PhoneWorkSelect = null;
		}

		private bool CanAddPhoneWork(object arg)
		{
			return PhoneWorkAdd != null && PhoneWorkAdd.Length > 0;
		}

		private void OnAddPhoneWork(object obj)
		{
			/*
			PhoneNumber phone = new(PhoneWorkAdd, PhoneType.Личный);
			PhonesWorkCreate.Add(phone);
			*/
		}
	}
}