using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPublisher.EventGrid
{
	public class EventGridPublisher : IEventGridPublisher
	{
		private readonly IConfiguration _configuration;
		private readonly EventGridClient _client;
		private readonly string _topicHostName;

		public EventGridPublisher(IConfiguration configuration)
		{
			_configuration = configuration;
			this._topicHostName = new Uri(_configuration["EventGrid:TopicHostName"]).Host;

			var topicCredentials = new TopicCredentials(_configuration["EventGrid:TopicKey"]);
			this._client = new EventGridClient(topicCredentials);
		}

		public async Task PublishTicketEvent(Ticket ticket)
		{
			await this._client.PublishEventsAsync(this._topicHostName, new List<EventGridEvent>() { this.WrapTicket(ticket) });
		}

		private EventGridEvent WrapTicket(Ticket ticket)
		{
			return new EventGridEvent()
			{
				Id = Guid.NewGuid().ToString(),
				EventType = ticket.GetType().Name,
				Data = ticket,
				EventTime = DateTime.Now,
				Subject = ticket.Subject,
				DataVersion = "2.0"
			};
		}
	}
}
