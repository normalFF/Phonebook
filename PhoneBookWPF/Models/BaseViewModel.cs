using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBookWPF.Models
{
	internal abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public bool Set<T>(ref T currentValue, T newValue, [CallerMemberName] string parameter = null)
		{
			if (Equals(currentValue, newValue)) return false;
			currentValue = newValue;
			OnPropertyChanged(parameter);
			return true;
		}

		public void OnPropertyChanged([CallerMemberName] string parameter = null)
		{
			PropertyChanged.Invoke(this, new PropertyChangedEventArgs(parameter));
		}
	}
}
