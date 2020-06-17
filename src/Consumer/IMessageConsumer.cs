using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
	public interface IMessageConsumer
	{
		void RegisterOnMessageHandlerAndReceiveMessages();
		
		Task CloseSubscriptionClientAsync();
	}
}
