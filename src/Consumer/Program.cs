using Microsoft.ApplicationInsights.WorkerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Consumer
{
	/// <summary>
	/// The class that is the entry point for the <see cref="BackgroundService"/>. Note that this exactly looks like
	/// a .NET Core Web API.
	/// </summary>
	class Program
	{
		static async Task Main(string[] args)
		{
			await CreateHostBuilder(args).Build().RunAsync();
		}

		/// <summary>
		/// Builds a host object that holds the BackgroundService (Generic Host)
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		private static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					// add a specialized AI implementation for the Background/WorkerService
					// this is needed because this basically represents a console app which has no notion 
					// of HTTP requests.
					services.AddApplicationInsightsTelemetryWorkerService(new ApplicationInsightsServiceOptions()
					{
						DeveloperMode = true
					});

					// Tell the generic host which business logic to run
					services.AddHostedService<Service>();

					// instantiated on first request and reused
					services.AddSingleton<IMessageConsumer, MessageConsumer>();

					// is instantiated on each request. lightweight object without any state
					services.AddTransient<IProcessData, ProcessData>();
				});
		}
	}
}
