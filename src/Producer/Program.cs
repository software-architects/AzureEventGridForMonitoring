using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api
{
	/// <summary>
	/// The class that is the entry point for the API controller. Note that this exactly looks like
	/// a classic Console App
	/// </summary>
	public class Program
	{
		public static void Main(string[] args)
		{
			// builds that host object where the WebHost runs in
			// configure DI, Configuration,etc.
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// Builds a host object that holds the WebHost
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					// use the StartUp class to further configure this WebHost
					webBuilder.UseStartup<Startup>();
				});
	}
}
