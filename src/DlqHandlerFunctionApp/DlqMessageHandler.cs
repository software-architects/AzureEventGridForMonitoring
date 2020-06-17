// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Azure.Storage.Blob;

namespace DlqHandlerFunctionApp
{
	/// <summary>
	/// A class representing the function to handle dead letters from service bus.
	/// </summary>
	public static class DlqMessageHandler
	{
		/// <summary>
		/// The method that is executed whenever an eventgrid message is pushed to the function
		/// </summary>
		/// <param name="eventGridEvent">The event grid that was forwarded to the function.</param>
		/// <param name="stream">The stream to write a blob to</param>
		/// <param name="log">An instance to log stuff.</param>
		[FunctionName("DlqMessageHandler")]
		public static void Run(
			[EventGridTrigger]EventGridEvent eventGridEvent,
			[Blob("events/{sys.utcnow}", FileAccess.Write)] Stream stream,
			ILogger log)
		{
			log.LogInformation(eventGridEvent.Data.ToString());

			using (var streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write(eventGridEvent.Data);
				streamWriter.Flush();
			}
		}
	}
}
