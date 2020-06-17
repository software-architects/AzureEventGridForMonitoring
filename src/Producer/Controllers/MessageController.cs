using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;

namespace Api.Controllers
{
	/// <summary>
	/// A class that represents an API that receives instances of <see cref="SimpleMessage"/>. On receive,
	/// the API sends a message to a configured instance of service bus.
	/// 
	/// The "Route" attribute indicates that the route in the rule should be the "Message" part of the 
	/// MessageContoller classname
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class MessageController : ControllerBase
	{
		/// <summary>
		/// An instance of <see cref="MessageProducer"/> that provides methods to send messages to a service bus.
		/// It suffices to define a readonly field that holds the instance of <see cref="MessageProducer"/>. The 
		/// Dependency Injection mechanism of the generic host takes care of the instantiation using 
		/// constructor injetction.
		/// </summary>
		private readonly MessageProducer serviceBusTopicSender;

		/// <summary>
		/// Creates an instance of the <see cref="MessageController"/> class.
		/// </summary>
		/// <param name="serviceBusTopicSender"></param>
		public MessageController(MessageProducer serviceBusTopicSender)
			=> (this.serviceBusTopicSender) = (serviceBusTopicSender);

		/// <summary>
		/// A controller method that takes an instance of <see cref="SimpleMessage"/> as input and sends it to 
		/// a service bus.
		/// 
		/// API method is configured using attributes
		/// </summary>
		/// <param name="message">An instance of <see cref="SimpleMessage"/></param>
		/// <returns>The instance of <see cref="SimpleMessage"/> that was sent to the API.</returns>
		///
		[HttpPost]
		public async Task<IActionResult> Send([FromBody][Required] SimpleMessage message)
		{
			// send the message to service bus
			await serviceBusTopicSender.SendMessage(message);
			return Ok(message);
		}

		[HttpGet]
		public IEnumerable<SimpleMessage> Get()
		{
			return null;
		}
	}
}
