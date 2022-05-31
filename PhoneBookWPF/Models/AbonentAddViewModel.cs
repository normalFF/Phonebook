using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LibraryOOP;
using PhoneBookWPF.Command;

namespace PhoneBookWPF.Models
{
	internal class AbonentAddViewModel : BaseViewModel
	{
		private string _title;
		private PhoneBook _phoneBook;
		private IEnumerable<string> _abonentsGroups;
		private string _currentGroup;

		private string _nameCreate;
		private string _surnameCreate;
		private string _residentCreate;
		private DateTime? _dateTimeCreate;
		private ObservableCollection<PhoneNumber> _phonesCreate;
		private string _phoneAdd;
		private string _newTypePhone;
		private string _selectTypePhone;
		private PhoneNumber _phoneSelect;

		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
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
		public ObservableCollection<PhoneNumber> PhonesCreate
		{
			get => _phonesCreate;
			set => Set(ref _phonesCreate, value);
		}
		public string PhoneAdd
		{
			get => _phoneAdd;
			set => Set(ref _phoneAdd, value);
		}
		public string NewTypePhone
		{
			get => _newTypePhone;
			set => Set(ref _newTypePhone, value);
		}
		public string SelectTypePhone
		{
			get => _selectTypePhone;
			set => Set(ref _selectTypePhone, value);
		}
		public PhoneNumber PhoneSelect
		{
			get => _phoneSelect;
			set => Set(ref _phoneSelect, value);
		}
		public IEnumerable<string> AbonentsGroups
		{
			get => _abonentsGroups;
			set => Set(ref _abonentsGroups, value);
		}
		public string CurrentGroup
		{
			get => _currentGroup;
			set => Set(ref _currentGroup, value);
		}

		private ObservableCollection<string> _phoneTypes;
		private ObservableCollection<string> _selectedNamesGroups;
		private string _selectedNameGroup;

		public ObservableCollection<string> PhoneTypes
		{
			get => _phoneTypes;
			set => Set(ref _phoneTypes, value);
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

		public ICommand ClearAbonentCommand { get; private set; }
		public ICommand AddPhoneCommand { get; private set; }
		public ICommand DeletePhoneCommand { get; private set; }
		public ICommand AddNameInSelectedGroup { get; private set; }
		public ICommand RemoveNameInSelectedGroup { get; private set; }

		private Visibility _visible;
		public Visibility Visibility
		{
			get => _visible;
			set => Set(ref _visible, value);
		}

		public AbonentAddViewModel(MainViewModel model)
		{
			InitializeModel(model);
			InitializeCommand();
		}

		private void InitializeModel(MainViewModel model)
		{
			_phoneBook = PhoneBook.GetPhoneBook();

			if (model.AbonentDialogParameter == AbonentDialogEnum.Create)
			{
				Title = "Добавление Абонента";
				SelectedNamesGroups = new ObservableCollection<string>();
				Visibility = Visibility.Collapsed;
			}
			else if (model.AbonentDialogParameter == AbonentDialogEnum.Edit)
			{
				Title = "Редактирование данных Абонента";
				SetProperties(model);
				Visibility = Visibility.Collapsed;
			}
			else
			{
				Title = "Информация об Абоненте";
				SetProperties(model);
				Visibility = Visibility.Visible;
			}

			AbonentsGroups = _phoneBook.AbonentsGroup;
			PhoneTypes = new ObservableCollection<string>(_phoneBook.PhoneType.ToList());
		}

		private void SetProperties(MainViewModel model)
		{
			SelectedNamesGroups = new ObservableCollection<string>(model.SelectedAbonent.Groups.ToList());
			NameCreate = model.SelectedAbonent.Name;
			SurnameCreate = model.SelectedAbonent.Surname;
			DateTimeCreate = model.SelectedAbonent.DateOfBirth;
			ResidentCreate = model.SelectedAbonent.Residence;
			SelectedNamesGroups = new ObservableCollection<string>(model.SelectedAbonent.Groups.ToList());
			PhonesCreate = new ObservableCollection<PhoneNumber>(model.SelectedAbonent.PhoneNumbers.ToList());
		}

		private void InitializeCommand()
		{
			ClearAbonentCommand = new CommandBase(OnClearAbonent, CanClearAbonent);
			AddPhoneCommand = new CommandBase(OnAddPhone, CanAddPhone);
			DeletePhoneCommand = new CommandBase(OnDeletePhone, CanDeletePhone);
			AddNameInSelectedGroup = new CommandBase(OnAddNameGroup, CanAddNameGroup);
			RemoveNameInSelectedGroup = new CommandBase(OnRemoveNameGroup, CanRemoveNameGroup);
		}

		private bool CanRemoveNameGroup(object arg) => !string.IsNullOrEmpty(SelectedNameGroup);

		private void OnRemoveNameGroup(object obj)
		{
			foreach (string item in SelectedNamesGroups)
			{
				if (string.Equals(item, SelectedNameGroup, StringComparison.Ordinal))
				{
					SelectedNamesGroups.Remove(item);
					SelectedNameGroup = null;
					OnPropertyChanged(nameof(AbonentsGroups));
					return;
				}
			}
			SelectedNameGroup = null;
		}

		private bool CanDeletePhone(object obj) => PhoneSelect != null;

		private void OnDeletePhone(object obj)
		{
			PhonesCreate.Remove(PhoneSelect);
			PhoneSelect = null;
		}

		private bool CanAddNameGroup(object arg) => !string.IsNullOrEmpty(CurrentGroup);

		private void OnAddNameGroup(object obj)
		{
			foreach (string item in AbonentsGroups)
			{
				if (string.Equals(item, CurrentGroup, StringComparison.Ordinal))
				{
					SelectedNamesGroups.Add(CurrentGroup);
					CurrentGroup = null;
					OnPropertyChanged(nameof(AbonentsGroups));
					return;
				}
			}
			CurrentGroup = null;
		}

		private bool CanAddPhone(object obj) => !string.IsNullOrEmpty(PhoneAdd);

		private void OnAddPhone(object obj)
		{
			if (PhoneNumber.IsCorrectPhone(PhoneAdd))
			{
				PhoneNumber phone = PhoneBook.CreatePhoneNumber(PhoneAdd, string.IsNullOrEmpty(NewTypePhone) == true ? SelectTypePhone : NewTypePhone);

				foreach (PhoneNumber item in PhonesCreate)
				{
					if (item.Equals(phone))
					{
						MessageBox.Show($"Телефон «{PhoneAdd}» уже содердится в данном списке", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
						return;
					}
				}
				PhonesCreate.Add(phone);
				NewTypePhone = null;
				PhoneAdd = null;
			}
			else
			{
				MessageBox.Show($"Значение «{PhoneAdd}» является недопустимым для номера телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private bool CanClearAbonent(object obj) => true;

		private void OnClearAbonent(object obj)
		{
			NameCreate = null;
			SurnameCreate = null;
			ResidentCreate = null;
			DateTimeCreate = null;
			PhonesCreate.Clear();
			PhoneAdd = null;
			NewTypePhone = null;
			SelectTypePhone = null;
			PhoneSelect = null;
			SelectedNamesGroups.Clear();
			OnPropertyChanged(nameof(AbonentsGroups));
		}
	}
}