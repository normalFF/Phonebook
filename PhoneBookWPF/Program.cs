using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PhoneBookWPF
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			var application = new App();
			application.InitializeComponent();
			application.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] Args)
		{
			IHostBuilder host = Host.CreateDefaultBuilder(Args);

			host.UseContentRoot(Environment.CurrentDirectory);
			host.ConfigureAppConfiguration((host, cfg) =>
			{
				cfg.SetBasePath(Environment.CurrentDirectory);
			});
			host.ConfigureServices(App.ConfigureServices);

			return host;
		}
	}
}
