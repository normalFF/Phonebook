using LibraryOOP;
using PhoneBookWPF.Command;
using PhoneBookWPF.DialogWindow;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PhoneBookWPF.Models
{
	internal partial class MainViewModel
	{
		public ICommand CreateAbonentWindow { get; private set; }
		public ICommand ViewAbonentWindow { get; private set; }
		public ICommand EditAbonentWindow { get; private set; }
		public ICommand CopyInfoAbonent { get; private set; }
		public ICommand DeleteAbonent { get; private set; }

		private void CommandInitialization()
		{
			CreateAbonentWindow = new CommandBase(OnAddAbonentWindow, CanAddAbonentWindow);
			ViewAbonentWindow = new CommandBase(OnViewAbonentWindow, CanViewAbonentWindow);
			EditAbonentWindow = new CommandBase(OnEditAbonentWindow, CanEditAbonentWindow);
			CopyInfoAbonent = new CommandBase(OnCopyInfoAbonent, CanCopyInfoAbonent);
			DeleteAbonent = new CommandBase(OnDeleteAbonent, CanDeleteAbonent);
		}

		private bool CanAddAbonentWindow(object arg) => true;

		private void OnAddAbonentWindow(object arg)
		{
			AbonentDialogParameter = AbonentDialogEnum.Create;
			AddUserWindow userWindow = new();
			userWindow.ShowDialog();
			OnPropertyChanged(nameof(Abonents));
			OnPropertyChanged(nameof(AbonentsGroups));
			OnPropertyChanged(nameof(PhoneTypes));
		}

		private bool CanViewAbonentWindow(object arg) => SelectedAbonent != null;

		private void OnViewAbonentWindow(object arg)
		{
			AbonentDialogParameter = AbonentDialogEnum.View;
			AddUserWindow userWindow = new();
			userWindow.ShowDialog();
		}

		private bool CanEditAbonentWindow(object arg) => SelectedAbonent != null;

		private void OnEditAbonentWindow(object arg)
		{
			AbonentDialogParameter = AbonentDialogEnum.Edit;
			AddUserWindow userWindow = new();
			userWindow.ShowDialog();
			OnPropertyChanged(nameof(Abonents));
			OnPropertyChanged(nameof(AbonentsGroups));
			OnPropertyChanged(nameof(PhoneTypes));
		}

		private bool CanCopyInfoAbonent(object arg) => SelectedAbonent != null;

		private void OnCopyInfoAbonent(object arg)
		{
			Clipboard.SetText(SelectedAbonent.ToString());
		}

		private bool CanDeleteAbonent(object arg) => SelectedAbonent != null;

		private void OnDeleteAbonent(object arg)
		{
			if (_phoneBook.RemoveAbonent(SelectedAbonent))
			{
				MessageBox.Show($"Абонент удалён из списка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBox.Show($"Абонент не найден в списке", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			SelectedAbonent = null;
			OnPropertyChanged(nameof(Abonents));
			OnPropertyChanged(nameof(AbonentsGroups));
			OnPropertyChanged(nameof(PhoneTypes));
		}
	}
}
