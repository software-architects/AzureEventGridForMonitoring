// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace EventConsumerFunctionApp
{
    public static class EventConsumerFunctionApp
    {
        [FunctionName("EventConsumerFunction")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, [SendGrid(ApiKey = "SendgridApiKey")] out SendGridMessage message, ILogger log)
        {
            log.LogInformation(eventGridEvent.Data.ToString());

            message = new SendGridMessage();
            message.AddTo("support@timecockpit.com");
            message.AddContent("text/html", eventGridEvent.Data.ToString());
            message.SetFrom(new EmailAddress("alexander@timecockpit.com"));
            message.SetSubject("Hi Team! Please ignore this ticket. KR Alex");
        }
    }
}
