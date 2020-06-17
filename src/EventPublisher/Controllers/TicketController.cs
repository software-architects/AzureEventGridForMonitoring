using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EventPublisher.EventGrid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EventPublisher.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TicketController : ControllerBase
	{
		private readonly IEventGridPublisher _eventGridPublisher;

		public TicketController(IEventGridPublisher eventGridPublisher)
		{
			_eventGridPublisher = eventGridPublisher;
		}

		[HttpPost]
		public async Task<IActionResult> Publish([FromBody][Required] Ticket ticket)
		{
			await _eventGridPublisher.PublishTicketEvent(ticket);
			return Ok(ticket);
		}

		[HttpGet]
		public IEnumerable<Ticket> Get()
		{
			return new List<Ticket>() { new Ticket() };
		}
	}
}
