using PhoneBookWPF.Command;
using PhoneBookWPF.DialogWindow;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System;

namespace PhoneBookWPF.Models
{
	internal partial class MainViewModel
	{
		private OpenFileDialog _openFile;
		private SaveFileDialog _saveFile;

		public ICommand CreateAbonentWindow { get; private set; }
		public ICommand ViewAbonentWindow { get; private set; }
		public ICommand EditAbonentWindow { get; private set; }
		public ICommand CopyInfoAbonent { get; private set; }
		public ICommand DeleteAbonent { get; private set; }

		public ICommand LoadFile { get; set; }
		public ICommand SaveFile { get; set; }
		public ICommand SaveFileAs { get; set; }
		public ICommand CreateFile { get; set; }

		private void CommandInitialization()
		{
			CreateAbonentWindow = new CommandBase(OnAddAbonentWindow, (arg) => true);
			ViewAbonentWindow = new CommandBase(OnViewAbonentWindow, (arg) => SelectedAbonent != null);
			EditAbonentWindow = new CommandBase(OnEditAbonentWindow, (arg) => SelectedAbonent != null);
			CopyInfoAbonent = new CommandBase(OnCopyInfoAbonent, (arg) => SelectedAbonent != null);
			DeleteAbonent = new CommandBase(OnDeleteAbonent, (arg) => SelectedAbonent != null);

			LoadFile = new CommandBase(OnOpenFile, (arg) => true);
			SaveFile = new CommandBase(OnSaveFile, (arg) => true);
			SaveFileAs = new CommandBase(OnSaveFileAs, (arg) => true);
			CreateFile = new CommandBase(OnCreateFile, (arg) => true);
		}

		private void OnAddAbonentWindow(object arg)
		{
			AbonentDialogParameter = AbonentDialogEnum.Create;
			AddUserWindow userWindow = new();
			userWindow.ShowDialog();
			OnPropertyChanged(nameof(Abonents));
			OnPropertyChanged(nameof(AbonentsGroups));
			OnPropertyChanged(nameof(PhoneTypes));
		}

		private void OnViewAbonentWindow(object arg)
		{
			AbonentDialogParameter = AbonentDialogEnum.View;
			AddUserWindow userWindow = new();
			userWindow.ShowDialog();
		}

		private void OnEditAbonentWindow(object arg)
		{
			AbonentDialogParameter = AbonentDialogEnum.Edit;
			AddUserWindow userWindow = new();
			userWindow.ShowDialog();
			OnPropertyChanged(nameof(Abonents));
			OnPropertyChanged(nameof(AbonentsGroups));
			OnPropertyChanged(nameof(PhoneTypes));
		}

		private void OnCopyInfoAbonent(object arg)
		{
			Clipboard.SetText(SelectedAbonent.ToString());
		}

		private void OnDeleteAbonent(object arg)
		{
			if (MessageBox.Show($"Вы точно хотите удалить абонента «{SelectedAbonent.Name} {SelectedAbonent.Surname}» из списка?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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


		private void OnOpenFile(object arg)
		{
			if (!_phoneBook.IsSaved())
			{
				MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

				switch (result)
				{
					case MessageBoxResult.Yes:
						if (!SaveChanges()) return;
						break;
					case MessageBoxResult.Cancel:
						return;
				}
			}

			if (_openFile.ShowDialog() == false) return;
			_fileWay = _openFile.FileName;
			DataInitialization(_fileWay);
		}

		private void OnSaveFile(object arg)
		{
			SaveChanges();
		}

		private void OnSaveFileAs(object arg)
		{
			if (_saveFile.ShowDialog() == false) return;
			_fileWay = _saveFile.FileName;

			SaveChanges();
		}

		private void OnCreateFile(object arg)
		{
			if (!_phoneBook.IsSaved())
			{
				MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Внимание", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

				switch (result)
				{
					case MessageBoxResult.Yes:
						if (!SaveChanges()) return;
						break;
					case MessageBoxResult.Cancel:
						return;
				}
			}

			DataInitialization();
		}

		private bool SaveChanges()
		{
			if (string.IsNullOrEmpty(_fileWay))
			{
				if (_saveFile.ShowDialog() == false) return false;
				_fileWay = _saveFile.FileName;
			}

			try
			{
				_phoneBook.SaveData(_fileWay);
				return true;
			}
			catch (Exception ex)
			{
				string message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
				MessageBox.Show($"Ошибка при сохранении файла:\n\r{message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
		}
	}
}
