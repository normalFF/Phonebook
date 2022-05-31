using Microsoft.Extensions.DependencyInjection;

namespace PhoneBookWPF.Models
{
	internal class ModelLocator
	{
		public MainViewModel MainView => App.Host.Services.GetRequiredService<MainViewModel>();

		public AbonentAddViewModel AbonentView => App.Host.Services.GetRequiredService<AbonentAddViewModel>();
	}
}
