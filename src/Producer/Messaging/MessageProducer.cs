using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Api.Messaging
{
	/// <summary>
	/// This class provides methods to send a message to a service bus topic
	/// </summary>
	public class MessageProducer
	{
		/// <summary>
		/// An instance of TopicClient for accessing the service bus
		/// </summary>
		private readonly TopicClient topicClient;

		/// <summary>
		/// Configuration object to access app configuration
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		///  Generic logger. Can be console, AI etc.
		/// </summary>
		private readonly ILogger logger;

		public MessageProducer(IConfiguration configuration,
			ILogger<MessageProducer> logger)
		{
			this.configuration = configuration;
			this.logger = logger;

			// be aware that the connection string copied from the portal does not work out of the box.
			// "EntityPath=message-in" has to be removed
			topicClient = new TopicClient(
				this.configuration["ServiceBus:MessageInTopicConnectionString"],
				this.configuration["ServiceBus:TopicName"]);
		}

		/// <summary>
		/// Serializes an instance of <see cref="SimpleMessage"/> and sends it to a service bus.
		/// </summary>
		/// <param name="payload">An instance of <see cref="SimpleMessage"/></param>
		/// <returns>An empty task.</returns>
		public async Task SendMessage(SimpleMessage payload)
		{
			string data = JsonConvert.SerializeObject(payload);

			// pass the JSON serialized message to a service bus message.
			var message = new Message(Encoding.UTF8.GetBytes(data));

			try
			{
				// send the message.
				await topicClient.SendAsync(message);
			}
			catch (Exception e)
			{
				logger.LogError(e.Message);
			}
		}
	}
}
