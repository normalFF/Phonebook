using LibraryOOP;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
		private readonly PhoneBook _phoneBook;
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
			_phoneBook = PhoneBook.GetPhoneBook();
			GetData.GetListAbonents(_phoneBook, 20);
			Abonents = _phoneBook.Abonents;
			AbonentsGroups = _phoneBook.AbonentsGroup;
			PhoneTypes = new ObservableCollection<string>(_phoneBook.PhoneType.ToList());

			CommandInitialization();
		}
	}
}