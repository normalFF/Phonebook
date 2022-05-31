using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System;
using PhoneBookWPF.Models;

namespace PhoneBookWPF
{
	public partial class App : Application
	{
		private static IHost _host;
		public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

		public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
		{
			services.AddSingleton<MainViewModel>();
			services.AddTransient<AbonentAddViewModel>();
		}

		protected override async void OnStartup(StartupEventArgs e)
		{
			IHost host = Host;
			base.OnStartup(e);
			await host.StartAsync().ConfigureAwait(false);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			IHost host = Host;
			await host.StopAsync().ConfigureAwait(false);
			host.Dispose();
			base.OnExit(e);
		}
	}
}
