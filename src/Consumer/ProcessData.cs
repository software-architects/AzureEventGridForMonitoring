using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
	public class ProcessData : IProcessData
	{
		private ILogger<ProcessData> Logger { get; }

		public ProcessData(ILogger<ProcessData> logger)
		{
			this.Logger = logger;
		}

		public async Task Process(SimpleMessage message)
		{
			if (message.MessageType == SimpleMessageType.SendToDlq)
			{
				throw new Exception("Could not process message");
			}

			Logger.LogInformation(JsonConvert.SerializeObject(message));
		}
	}
}
