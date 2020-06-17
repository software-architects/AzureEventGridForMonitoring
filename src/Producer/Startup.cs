using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
	/// <summary>
	/// A class that is used to configure an instance of <see cref="IWebHost"/>.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// A private field that holds the app's configuration. 
		/// It is instantiated by the WebHost's DI mechanism.
		/// </summary>
		public readonly IConfiguration configuration;

		/// <summary>
		/// Intantiates a class of <see cref="Startup"/>.
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">An instance of <see cref="IServiceCollection"/> that holds all services that are to be used
		/// during the lifetime of the API.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			// adds everything you need to use Application Insights for logging.
			services.AddApplicationInsightsTelemetry();

			// adds support for controllers and API-related features, but not views or page
			services.AddControllers();

			// Scoped objects are the same within a request, but different across different requests.
			services.AddScoped<MessageProducer>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
