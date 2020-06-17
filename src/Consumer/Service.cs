using Consumer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

internal class Service : BackgroundService
{
	private readonly ILogger<Service> logger;

	// a message consumer provides methods to handle events 
	// received from event grid
	private readonly IMessageConsumer messageConsumer;

	public Service(ILogger<Service> logger, IMessageConsumer messageConsumer)
		=> (this.logger, this.messageConsumer) = (logger, messageConsumer);

	/// <summary>
	/// Methods that needs to be overridden from <see cref="BackgroundService"/>.
	/// This method is called when the app is start up.
	/// </summary>
	/// <param name="stoppingToken"></param>
	/// <returns></returns>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			messageConsumer.RegisterOnMessageHandlerAndReceiveMessages();
			await Task.Delay(1000, stoppingToken);
		}
	}
}