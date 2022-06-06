using LibraryOOP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PhoneBookWPF.Models
{
	internal enum AbonentDialogEnum
	{
		Create,
		Edit,
		View
	}

	internal partial class MainViewModel : BaseViewModel
	{
		private string _fileWay;
		private PhoneBook _phoneBook;
		private IEnumerable<Abonent> _abonents;
		private IEnumerable<string> _abonentsGroups;
		private ObservableCollection<string> _phoneTypes;
		private string _selectedGroupName;
		private string _currentGroup;
		private Abonent _selectedAbonent;

		public IEnumerable<Abonent> Abonents
		{
			get => _abonents;
			set => Set(ref _abonents, value);
		}
		public IEnumerable<string> AbonentsGroups
		{
			get => _abonentsGroups;
			set => Set(ref _abonentsGroups, value);
		}
		public ObservableCollection<string> PhoneTypes
		{
			get => _phoneTypes;
			set => Set(ref _phoneTypes, value);
		}
		public string SelectedGroupName
		{
			get => _selectedGroupName;
			set => Set(ref _selectedGroupName, value);
		}
		public string CurrentGroup
		{
			get => _currentGroup;
			set => Set(ref _currentGroup, value);
		}
		public Abonent SelectedAbonent
		{
			get => _selectedAbonent;
			set => Set(ref _selectedAbonent, value);
		}

		private AbonentDialogEnum _abonentDialogParameter;
		public AbonentDialogEnum AbonentDialogParameter
		{
			get => _abonentDialogParameter;
			set => Set(ref _abonentDialogParameter, value);
		}

		public MainViewModel()
		{
			ModelInitialization();
			CommandInitialization();
		}

		private void ModelInitialization()
		{
			_phoneBook = PhoneBook.GetPhoneBook();
			Abonents = _phoneBook.Abonents;
			AbonentsGroups = _phoneBook.AbonentsGroup;
			PhoneTypes = new ObservableCollection<string>(_phoneBook.PhoneType);
			_saveFile = new()
			{
				Filter = "(*.json)|*.json",
				CreatePrompt = true,
				AddExtension = true
			};
			_openFile = new()
			{
				Filter = "(*.json)|*.json",
				AddExtension = true
			};
		}

		private void DataInitialization(string fileWay = null)
		{
			PhoneBook.DeletePhoneBook();
			_phoneBook = PhoneBook.GetPhoneBook();

			if (!string.IsNullOrEmpty(fileWay))
			{
				try
				{
					_phoneBook.LoadData(_fileWay);
				}
				catch (Exception ex)
				{
					string message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
					MessageBox.Show($"Ошибка при открытии файла: \n\r{message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}
			}

			Abonents = _phoneBook.Abonents;
			AbonentsGroups = _phoneBook.AbonentsGroup;
			PhoneTypes = new ObservableCollection<string>(_phoneBook.PhoneType.ToList());
		}
	}
}