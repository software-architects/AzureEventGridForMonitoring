# Azure Service Bus

[doc](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview)

## Comparison

* [Service Bus Messaging](https://docs.microsoft.com/en-us/azure/service-bus-messaging/)
* [Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/)
* [Event Hub](https://docs.microsoft.com/en-us/azure/event-hubs/) (not covered here)
* [Choose between Azure messaging services](https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services#comparison-of-services)

## General

* Enterprise integration message broker (that is, it stores messages) for decoupling services
* Binary messages in binary format that contain JSON, XML, or just text
* Perfectly suited for high-value business transactions
* Stable service, no major new features announced (see also [team blog](https://azure.microsoft.com/en-us/blog/tag/azure-service-bus/))
* Primary protocol is [AMQP](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-amqp-overview) the [*Advanced Message Queuing Protocol (AMQP)*](https://en.wikipedia.org/wiki/Advanced_Message_Queuing_Protocol)
  * ISO/IEC Standard
  * SBMP is [phased out](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-amqp-protocol-guide#:~:text=The%20Advanced%20Message%20Queueing%20Protocol,Messaging%20and%20Azure%20Event%20Hubs.)
  * Possible to reduce lock-in effect (e.g. [RabbitMQ with Plugin](https://github.com/rabbitmq/rabbitmq-amqp1.0/blob/v3.7.x/README.md))
* [Many advanced features](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview#advanced-features)
  * [Message sessions] (https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-sessions) --> ensure FIFO/ordered delivery
  * [DLQ](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues#moving-messages-to-the-dlq)
  * [Duplicate detection](https://docs.microsoft.com/en-us/azure/service-bus-messaging/duplicate-detection)
* Recommendation: Use *Premium* tier for production environments ([docs](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-premium-messaging))
  * Predictable performance
  * Larger messages
  * Keep [pricing](https://azure.microsoft.com/en-us/pricing/details/service-bus/) in mind
  * Event Grid integration!!!
* ARM References
  * [Azure Service Bus Namespaces](https://docs.microsoft.com/en-us/azure/templates/microsoft.servicebus/2017-04-01/namespaces)

> [Sample for creating resource group with ARM template](azuredeploy-resource-group.json)

## Queues vs. Topics vs. Subscriptions

### Queues
![Queues](https://docs.microsoft.com/en-us/azure/service-bus-messaging/media/service-bus-messaging-overview/about-service-bus-queue.png)

* 1:1 relationship (point-to-point) between producer and consumer
* Ordered and timestamped on arrival
* Pull model, not push
* No subscriptions needed (because 1:1)

### Topics
![Topics and Subscriptions](https://docs.microsoft.com/en-us/azure/service-bus-messaging/media/service-bus-messaging-overview/about-service-bus-topic.png)

* 1:N (publish-subscribe model)
* Topics can have N subscriptions (broadcast)
* Each topic subscriber gets copy of message

### Subscriptions
* Indicates that a consumer is interested in topic
* Subscriptions can expire/get autodeleted
* per default all subscriptions of topic get copy of messages
  * --> Filter

## Filters
* All filters evaluate message properties, **filters can't evaluate the message body**
* Create rules to define what interests a subscriptions
  * Booean filters
  * SQL-ish filters
  * Correlation Filters
* Performance penality
* Filters enable [partitioning and routing](https://docs.microsoft.com/en-us/azure/service-bus-messaging/topic-filters#usage-patterns)
* [Actions](https://docs.microsoft.com/en-us/azure/service-bus-messaging/topic-filters#actions) --> allow to alter messages (add, remove, replace properties)

## Dead-Letter Queues (DLQ)
* [doc](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues)
* [DLQ example on github](https://github.com/Azure/azure-service-bus/tree/master/samples/DotNet/Microsoft.ServiceBus.Messaging/DeadletterQueue)
* Subqueue that always exists
* Holds messages that cannot be delivered/error occurred
* Messages cannot be directly send to DLQ
* Monitor DL messages and correct them -> requeue
* No TTL check/cleanup
* DLQ associated with subscription than topic
* [What triggers a DL](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues#moving-messages-to-the-dlq)
  * error in processing
  * call to `DeadLetterAsync`
  * Errors while processing subscription (filter) rules
* Viewing DLQ
  * [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/servicebus/topic/subscription?view=azure-cli-latest#az-servicebus-topic-subscription-show)
  * [Service Bus Explorer](https://github.com/paolosalvatori/ServiceBusExplorer/)

## Authentiation
* Authenticate request against service bus/queue/topic
* [Shared Access Signatures](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-authentication-and-authorization#shared-access-signature)
  * Token signed with cryptographic key + with rights
  * Key on service bus level applies to all entites (queues/topics)
  * Rights: `Listen, Send, Manage`
* [Managed Identity](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-authentication-and-authorization#azure-active-directory)
  * [Just use it](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-managed-service-identity)
  * MSI on queues/topics can only be created via CLI/ARM

## Security Features

* Limit access using [*shared access signatures*](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-sas)
* Use Azure AD to secure access to Service Bus (in particular [managed identities](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-managed-service-identity) are useful for that)
  * If not possible, store access secrets in [key vault](https://docs.microsoft.com/en-us/azure/key-vault/general/overview) or at least protect them properly (e.g. ASP.NET Core [Data Protection](https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-3.1))
* Limit access using [IP firewall rules](https://docs.microsoft.com/en-us/azure/service-bus-messaging/network-security#ip-firewall)
* Use [*private endpoints*](https://docs.microsoft.com/en-us/azure/service-bus-messaging/network-security#private-endpoints) to limit access to certain virtual networks
* Uses Azure Storage in the background, therefore always encrypted, MS-managed or [customer-managed keys](https://docs.microsoft.com/en-us/azure/service-bus-messaging/configure-customer-managed-key#enable-customer-managed-keys)

### Programming with Service Bus

* Collection of [.NET Samples](https://github.com/Azure/azure-service-bus/tree/master/samples/) on GitHub
* [`Message`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.servicebus.message) consisting of...
  * `Body`
  * Properties (`SystemProperties` and `UserProperties`)
* [Receive modes](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-queues-topics-subscriptions#receive-modes)
  * Receive and Delete
  * Peek Lock (consider checking [`DeliveryCount`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.messaging.brokeredmessage.deliverycount) or setting [`MaxDeliveryCount`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.messaging.queuedescription.maxdeliverycount) for poisoned message handling)
* [Filtered topics](https://docs.microsoft.com/en-us/azure/service-bus-messaging/topic-filters)
* [Message correlation](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messages-payloads#message-routing-and-correlation)
  * Request-Reply-Pattern using [`ReplyTo`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.servicebus.message.replyto)
* Payload serialization (see also [`ContentType`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.servicebus.message.contenttype)); e.g.:
  * JSON
  * ProtoBuf
* Message [sequencing](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-sequencing) and [lifetime control](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-expiration)
  * [Dead-letter queue](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dead-letter-queues)
  * [Sessions](https://docs.microsoft.com/en-us/azure/service-bus-messaging/message-sessions#first-in-first-out-fifo-pattern)
* Send events via Event Grid when messages are available ([docs](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-to-event-grid-integration-concept)) or messages are delivered to dead-letter queue
  * Prevents polling
* Consider [monitoring with Application Insights](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-end-to-end-tracing#tracking-with-azure-application-insights)
* Consider [Azure Serverless Functions](https://docs.microsoft.com/en-us/azure/azure-functions/) for message-driven applications