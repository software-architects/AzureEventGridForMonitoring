using Microsoft.ApplicationInsights;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
	/// <summary>
	/// A class that provides methods to consume instancs of <see cref="SimpleMessage"/>
	/// </summary>
	public class MessageConsumer : IMessageConsumer
	{
		private readonly IConfiguration configuration;
		private readonly SubscriptionClient subscriptionClient;
		private readonly ILogger logger;
		private readonly TelemetryClient telemetryClient;

		/// <summary>
		/// Creates an instance of <see cref="MessageConsumer"/>.
		/// This constructor is called by DI.
		/// </summary>
		/// <param name="processData">An interface that provides logic to further process a message.</param>
		/// <param name="configuration">The app's configuration. An instance of <see cref="IConfiguration"/> is injected by DI.</param>
		/// <param name="logger">A generic logger injected by DI.</param>
		/// <param name="telemetryClient">A fully fletched instance of <see cref="TelemetryClient"/> for further logging.</param>
		public MessageConsumer(IProcessData processData,
			IConfiguration configuration,
			ILogger<MessageConsumer> logger,
			TelemetryClient telemetryClient)
		{
			this.configuration = configuration;
			this.logger = logger;
			this.telemetryClient = telemetryClient;

			// instantiate an instance of a SubscriptionClient. 
			// The is used to handle messages
			subscriptionClient = new SubscriptionClient(
			   this.configuration["ServiceBus:MessageInTopicConnectionString"],
				this.configuration["ServiceBus:TopicName"],
				this.configuration["ServiceBus:SubscriptionName"], ReceiveMode.PeekLock);
		}

		public void RegisterOnMessageHandlerAndReceiveMessages()
		{
			var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
			{
				MaxConcurrentCalls = 1,
				AutoComplete = false // need to explicitly call CompleteAsync
			};

			// https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-azure-and-service-bus-queues-compared-contrasted#foundational-capabilities
			// https://stackoverflow.com/questions/56705141/azure-service-bus-topic-receiving-messages
			//  it will ask for a message and wait for it. If after a timeout period of one minute nothing is returned, it will poll again. 
			// In case a message is available before timeout expires, the message will be given to the message handler and polling will start again. 
			// Azure Service Bus does not push messages to the clients.
			subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
		}

		private async Task ProcessMessagesAsync(Message message, CancellationToken token)
		{
			// Deserialize the message to get the original json sent to the producer api
			var simpleMessage = JsonConvert.DeserializeObject<SimpleMessage>(Encoding.UTF8.GetString(message.Body));
			//await _processData.Process(simpleMessage);

			// decide what to communicate back to the service bus
			switch (simpleMessage.MessageType)
			{
				case SimpleMessageType.Complete:
					await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
					telemetryClient.TrackTrace($"Message with id {message.MessageId} successfully processed");
					break;
				case SimpleMessageType.SendToDlq:
					await subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken);
					telemetryClient.TrackTrace($"Message with id {message.MessageId} sent to DLQ");
					break;
				case SimpleMessageType.Error:
					// increments the delivery count
					throw new InvalidOperationException("Cannot process message");
				default:
					// increments the delivery count
					await subscriptionClient.AbandonAsync(message.SystemProperties.LockToken);
					telemetryClient.TrackTrace($"Message with id {message.MessageId} abandoned. Retrying shortly...");
					break;
			}
		}

		private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
		{
			logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
			var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

			logger.LogDebug($"- Endpoint: {context.Endpoint}");
			logger.LogDebug($"- Entity Path: {context.EntityPath}");
			logger.LogDebug($"- Executing Action: {context.Action}");

			return Task.CompletedTask;
		}

		public async Task CloseSubscriptionClientAsync()
		{
			await subscriptionClient.CloseAsync();
		}
	}
}