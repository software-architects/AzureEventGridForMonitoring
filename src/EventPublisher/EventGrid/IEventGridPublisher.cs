using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPublisher.EventGrid
{
	public interface IEventGridPublisher
	{
		Task PublishTicketEvent(Ticket ticket);
	}
}
