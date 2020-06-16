# Azure Event Grid for Monitoring Training

## Introduction

Many services in *Microsoft Azure* publish events in [*Event Grid*](https://docs.microsoft.com/en-us/azure/event-grid/) in case of important activities or potential errors. In this training, we want to explore Event Grid, understand how it works, and build samples for monitoring events in [*Azure Service Bus*](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) and [*Azure Blob Storage*](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction). These examples are especially relevant in case of integration scenarios like:

* Data exchange between on-premises applications via *Azure Service Bus*
* Data transfer from operational software to Data Warehouse solutions via *Azure Blob Storage*

## Agenda

1. Introduction into *Azure Service Bus Messaging*
   * Differences queues and topics/subscriptions
   * Brief introduction into *Shared Access Signatures*
   * Brief introduction into MSI
   * Specifically important for this training:
     * Dead-letter handling
     * Queues/subscriptions without receiver(s)
1. Introduction into *Azure Blob Storage*
   * Accounts, containers, and blobs
   * Brief introduction into *Shared Access Signatures*
1. Brief introduction into *Azure Functions*
   * Powershell and .NET
   * No deep-dive, only enough to be able to handle events with Functions
1. Brief introduction into *Azure Logic Apps*
   * No deep-dive, only enough to understand what Logic Apps are
1. Introduction into *Event Grid*
   * Brief, general introduction (no deep-dive)
   * Handling storage events with Event Grid ([docs](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-event-overview))
   * Handling service bus events with Event Grid ([docs](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-to-event-grid-integration-concept))
   * Handling events with Azure Functions ([docs](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-event-grid))
   * Custom topics ([creating](https://docs.microsoft.com/en-us/azure/event-grid/scripts/event-grid-cli-create-custom-topic), [subscribing](https://docs.microsoft.com/en-us/azure/event-grid/scripts/event-grid-cli-subscribe-custom-topic), [posting](https://docs.microsoft.com/en-us/azure/event-grid/post-to-custom-topic))

## Training Concept

The training will be a combination of theory and practical exercises. Your trainer will describe the concepts and demonstrate them in short examples and based on the Azure documentation. The practical part will consist of the following exercises:

* *Service Bus*:
  * Create a *Service Bus Namespace* with a *Topic* and a *Subscription* using an *ARM template*
  * Create an *Azure Function* that can receive events in case of Service Bus messages that could not be processed
  * Use *Event Grid* to connect the Service Bus' dead-letter queue with the created function
* *Azure Blob Storage*:
  * Create a *Storage Account* using an *ARM template*
  * Create an *Azure Function* that can receive events in case of created storage blobs (e.g. CSV file to upload into DWH)
  * Use *Event Grid* to connect the Azure Storage with the created function
* *Custom Events*
  * Create a custom topic for *Event Grid*. Applications can post events to this topic if they want to trigger an alert e.g. in Trouble Ticket solution.
  * Build a small sample app (e.g. ASP.NET Core Web API) that posts an event to the created topic.
  * Create an *Azure Function* that can receive custom events (could e.g. create the ticket in the Trouble Ticket solution)