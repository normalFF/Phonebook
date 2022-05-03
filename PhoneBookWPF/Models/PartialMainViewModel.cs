using LibraryOOP;
using PhoneBookWPF.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PhoneBookWPF.Models
{
	internal partial class MainViewModel
	{
		public ICommand AddAbonentCommand { get; private set; }
		public ICommand ClearAbonentCommand { get; private set; }
		public ICommand AddPhoneMainCommand { get; private set; }
		public ICommand DeletePhoneMainCommand { get; private set; }
		public ICommand AddPhoneHomeCommand { get; private set; }
		public ICommand DeletePhoneHomeCommand { get; private set; }
		public ICommand AddPhoneWorkCommand { get; private set; }
		public ICommand DeletePhoneWorkCommand { get; private set; }
		public ICommand AddNameInSelectedGroup { get; private set; }
		public ICommand RemoveNameInSelectedGroup { get; private set; }

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
			AddNameInSelectedGroup = new CommandBase(OnAddNameGroup, CanAddNameGroup);
			RemoveNameInSelectedGroup = new CommandBase(OnRemoveNameGroup, CanRemoveNameGroup);

		}

		private bool CanRemoveNameGroup(object arg) => !string.IsNullOrEmpty(SelectedNameGroup);

		private void OnRemoveNameGroup(object obj)
		{
			foreach (var item in SelectedNamesGroups)
			{
				if (string.Equals(item, SelectedNameGroup, StringComparison.Ordinal))
				{
					AllNamesGroups.Add(item);
					SelectedNamesGroups.Remove(item);
					SelectedNameGroup = null;
					return;
				}
			}
			SelectedNameGroup = null;
		}

		private bool CanAddNameGroup(object arg) => !string.IsNullOrEmpty(SelectedAllGroupName);

		private void OnAddNameGroup(object obj)
		{
			foreach (var item in AllNamesGroups)
			{
				if (string.Equals(item, SelectedAllGroupName, StringComparison.Ordinal))
				{
					SelectedNamesGroups.Add(SelectedAllGroupName);
					AllNamesGroups.Remove(SelectedAllGroupName);
					SelectedAllGroupName = null;
					return;
				}
			}
			SelectedAllGroupName = null;
		}

		private bool CanCreateAbonent(object obj)
		{
			return !string.IsNullOrEmpty(NameCreate) && !string.IsNullOrEmpty(SurnameCreate) &&
				((PhonesMainCreate.Count > 0) || (PhonesHomeCreate.Count > 0) || (PhonesWorkCreate.Count > 0));
		}

		private void OnCreateAbonent(object obj)
		{
			var numbers = new List<PhoneNumber>() { };
			if (PhonesMainCreate.Count > 0) numbers.AddRange(PhonesMainCreate);
			if (PhonesHomeCreate.Count > 0) numbers.AddRange(PhonesHomeCreate);
			if (PhonesWorkCreate.Count > 0) numbers.AddRange(PhonesWorkCreate);

			if (_phoneBook.AddAbonent(NameCreate, SurnameCreate, numbers, DateTimeCreate, ResidentCreate))
			{
				OnPropertyChanged(nameof(Abonents));
				OnPropertyChanged(nameof(AbonentsGroups));

				foreach (var item in SelectedNamesGroups)
				{
					foreach (var group in AbonentsGroups)
					{
						if (string.Equals(group.Name, item, StringComparison.OrdinalIgnoreCase))
						{
							_phoneBook.AddAbonentsGroup(group, Abonents.ToList()[_phoneBook.AbonentCount - 1]);
							break;
						}
					}
				}

				MessageBox.Show($"Абонент «{SurnameCreate} {NameCreate}» добавлен в телефонную книгу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
				OnClearAbonent(null);
			}
			else
			{
				MessageBox.Show($"Абонент «{SurnameCreate} {NameCreate}» уже есть в телефонной книге", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private bool CanClearAbonent(object obj) => true;

		private void OnClearAbonent(object obj)
		{
			NameCreate = null;
			SurnameCreate = null;
			ResidentCreate = null;
			DateTimeCreate = null;
			PhonesMainCreate.Clear();
			PhoneMainAdd = null;
			PhoneMainSelect = null;
			PhonesHomeCreate.Clear();
			PhoneHomeAdd = null;
			PhoneHomeSelect = null;
			PhonesWorkCreate.Clear();
			PhoneWorkAdd = null;
			PhoneWorkSelect = null;
			GenerateListNamesGroup();
			SelectedNamesGroups.Clear();
		}

		private bool CanAddPhoneMain(object obj) => !string.IsNullOrEmpty(PhoneMainAdd);

		private void OnAddPhoneMain(object obj) => OnAddPhone(PhonesMainCreate, PhoneMainAdd, PhoneType.Личный);

		private bool CanDeletePhoneMain(object obj) => PhoneMainSelect != null;

		private void OnDeletePhoneMain(object obj)
		{
			PhonesMainCreate.Remove(PhoneMainSelect);
			PhoneMainSelect = null;
		}

		private bool CanAddPhoneHome(object obj) => !string.IsNullOrEmpty(PhoneHomeAdd);

		private void OnAddPhoneHome(object obj) => OnAddPhone(PhonesHomeCreate, PhoneHomeAdd, PhoneType.Домашний);

		private bool CanDeletePhoneHome(object obj) => PhoneHomeSelect != null;

		private void OnDeletePhoneHome(object obj)
		{
			PhonesHomeCreate.Remove(PhoneHomeSelect);
			PhoneHomeSelect = null;
		}

		private bool CanDeletePhoneWork(object arg) => PhoneWorkSelect != null;

		private void OnDeletePhoneWork(object obj)
		{
			PhonesWorkCreate.Remove(PhoneWorkSelect);
			PhoneWorkSelect = null;
		}

		private bool CanAddPhoneWork(object arg) => !string.IsNullOrEmpty(PhoneWorkAdd);

		private void OnAddPhoneWork(object obj) => OnAddPhone(PhonesWorkCreate, PhoneWorkAdd, PhoneType.Рабочий);

		private static void OnAddPhone(ObservableCollection<PhoneNumber> phoneCollection, string newPhone, PhoneType type)
		{
			if (PhoneNumber.IsCorrectPhone(newPhone))
			{
				var phone = PhoneBook.CreatePhoneNumber(newPhone, type);

				foreach (var item in phoneCollection)
				{
					if (item.Equals(phone))
					{
						MessageBox.Show($"Телефон «{newPhone}» уже содердится в данном списке", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
						return;
					}
				}
				phoneCollection.Add(phone);
			}
			else
			{
				MessageBox.Show($"Значение «{newPhone}» является недопустимым для номера телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
