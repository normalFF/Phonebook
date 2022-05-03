using System;
using LibraryOOP;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Linq;

namespace PhoneBookWPF.Models
{
	internal partial class MainViewModel : BaseViewModel
	{
		//-----------------------------------------------
		private readonly PhoneBook _phoneBook;
		private IEnumerable<Abonent> _abonents;
		private IEnumerable<AbonentsGroup> _abonentsGroups;
		private string _selectedGroupName;
		private List<Abonent> _selectedGoupList;
		private Abonent _selectedAbonent;

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
		public string SelectedGroupName
		{
			get => _selectedGroupName;
			set
			{
				if (Set(ref _selectedGroupName, value))
				{
					foreach (var item in AbonentsGroups)
					{
						if (string.Equals(item.Name, SelectedGroupName))
							SelectedGoupList = item.Abonents.ToList();
					}
				}
			}
		}
		public List<Abonent> SelectedGoupList
		{
			get => _selectedGoupList;
			set => Set(ref _selectedGoupList, value);
		}
		public Abonent SelectedAbonent
		{
			get => _selectedAbonent;
			set => Set(ref _selectedAbonent, value);
		}

		//----------------------------------------------

		private ObservableCollection<string> _allNamesGroups;
		private string _selectedAllGroupName;
		private ObservableCollection<string> _selectedNamesGroups;
		private string _selectedNameGroup;

		public ObservableCollection<string> AllNamesGroups
		{
			get => _allNamesGroups;
			set => Set(ref _allNamesGroups, value);
		}
		public string SelectedAllGroupName
		{
			get => _selectedAllGroupName;
			set => Set(ref _selectedAllGroupName, value);
		}
		public ObservableCollection<string> SelectedNamesGroups
		{
			get => _selectedNamesGroups;
			set => Set(ref _selectedNamesGroups, value);
		}
		public string SelectedNameGroup
		{
			get => _selectedNameGroup;
			set => Set(ref _selectedNameGroup, value);
		}

		//----------------------------------------------

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
			set
			{
				if (value != null)
				{
					if (DateTime.Compare((DateTime)value, DateTime.Now) > 0)
					{
						MessageBox.Show($"Некорректная дата рождения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
						value = DateTime.Now;
					}
				}
				Set(ref _dateTimeCreate, value);
			}
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

			SelectedNamesGroups = new();
			GenerateListNamesGroup();
		}

		private void GenerateListNamesGroup()
		{
			AllNamesGroups = new();
			foreach (var item in AbonentsGroups)
			{
				AllNamesGroups.Add(item.Name);
			}
		}
	}
}