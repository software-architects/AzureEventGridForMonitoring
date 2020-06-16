# Event Grid

[doc](https://docs.microsoft.com/en-us/azure/event-grid/overview)
* [Compare messaging services](https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services#event-vs-message-services)

## General
* fully-managed [serverless](https://azure.microsoft.com/en-us/overview/serverless-computing/) model
* infrastructure for event-driven computing
* to build applications with event-based architecture
* focuses on events (messages)
* implicates that something !happened!
* publish-subscribe
* allows to send events to topics, events are routed to handlers
* near real-time delivery, large scale
* reliable: deployed over multiple [fault domains](https://azure.codefari.com/2018/12/what-are-availability-set-fault-domain.html)

## Use Cases
*  [Serverless](https://azure.microsoft.com/en-us/overview/serverless-computing/) application architecture
  * less need to manage infrastructure
  * automatic provisioning, scale and management

![alt](https://docs.microsoft.com/en-us/azure/event-grid/media/overview/serverless_web_app.png)

* Ops Automation
  * Notify eventhandler that something has been provisioned
  * E.g. a new VM has been created

* Application Integration
  * poor-man's messagebus
  * send some data in an event to the EG
  * built-in even hanlder like Logic Apps --> Cloud Workflow integration
  * Attention: [Compare messaging services](https://docs.microsoft.com/en-us/azure/event-grid/compare-messaging-services#event-vs-message-services)

## Key Features
* Simplicity (portal & arm)
* Azure integration (Built-in events)
* Custom events
* Filtering
* Multicast (Fanout)
* [reliable delivery](https://docs.microsoft.com/en-us/azure/event-grid/delivery-and-retry)
  * redelivery, if event handler is offline
  * retry for 24h
  * [DL blob storage](https://docs.microsoft.com/en-us/azure/event-grid/delivery-and-retry#dead-letter-events)
* platform-agnostic
  * not necessary to use SDK
  * message can be sent via HTTP to topic --> any language that supports HTTP
  * EG supports [WebHook Standard](https://docs.microsoft.com/en-us/azure/event-grid/handler-webhooks) Standard
* [pay-per-event]https://azure.microsoft.com/en-us/pricing/details/event-grid/)

## Concepts

[doc](https://docs.microsoft.com/en-us/azure/event-grid/concepts)

* Events - What happened.
* [Event sources](https://docs.microsoft.com/en-us/azure/event-grid/overview#event-sources) - Where the event took place (Storage Account, Service Bus...)
* Topics - A collection of related events. The endpoint where publishers send events.
  * Custom topics
  * System topics (see [lifecycle](https://docs.microsoft.com/en-us/azure/event-grid/system-topics#lifecycle-of-system-topics))
  * [Partner topics](https://docs.microsoft.com/en-us/azure/event-grid/partner-topics-overview)
* Event subscriptions - The endpoint or built-in mechanism to route events, sometimes to more than one handler. A subscription indicates to EG that somebody is interested in the event. EG routes an event to the subscription. Subscriptions are also used by handlers to intelligently filter incoming events. Multiple scenarios possible: 1:1, 1:N, ...
  * [Support expiration](https://docs.microsoft.com/en-us/azure/event-grid/concepts#event-subscriptions)
* [Event handlers](https://docs.microsoft.com/en-us/azure/event-grid/overview#event-handlers) - The app or service reacting to the event.
  * [built-in](https://docs.microsoft.com/en-us/azure/event-grid/overview#event-handlers)
* [Filtering](https://docs.microsoft.com/en-us/azure/event-grid/event-filtering)
   * Event type
   * Subject
     * Filter on subject
   * Advanced filtering
     * Filter on fields [schema](https://docs.microsoft.com/en-us/azure/event-grid/event-schema) and data

## Azure Integration
* Lots of services offer EG integration
* Services publish events to system topics
* Azure services to handle messages/events
* Azure services have predefinded topics
  * Service Bus: active messages with no listener, DLQ

![alt](https://docs.microsoft.com/en-us/azure/event-grid/media/overview/functional-model.png)

## Provisioning
* infrastructure for event-driven computing
* you do not need to create an EG yourself
* only topics and subscriptions need to be provisioned
* To hook up built-in services --> event grid system topics
* To hook up custom services --> event grid topics

## Security Features

* [Managed Identity!](https://docs.microsoft.com/en-us/azure/event-grid/managed-service-identity)
* Limit access using [*shared access signatures*](https://docs.microsoft.com/en-us/azure/event-grid/security-authentication#authenticate-publishing-clients-using-sas-or-key)
  System topics do not have SAS, because only Azure Services can publish there
* Use Azure AD to [secure Event Grid webhook endpoint](https://docs.microsoft.com/en-us/azure/event-grid/security-authentication#authenticate-event-delivery-to-webhook-endpoints)
* Store access secrets in [key vault](https://docs.microsoft.com/en-us/azure/key-vault/general/overview) or at least protect them properly (e.g. ASP.NET Core [Data Protection](https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-3.1))
* Use [RBAC to control access to Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/security-authorization)
* Limit possitibility to publish events using [IP firewall rules](https://docs.microsoft.com/en-us/azure/event-grid/network-security#ip-firewall)
* Use [*private endpoints*](https://docs.microsoft.com/en-us/azure/event-grid/network-security#private-endpoints) to keep events inside your virtual network

## Schemas
* [Event Grid Schema](https://docs.microsoft.com/en-us/azure/event-grid/event-schema): proprietary schema to wrap messages
* Cloud Event Schema v1.0: cooperation with [Cloud Native Computing Foundation (CNCF)](https://jlik.me/e1x)
  * Potential to reduce lock-in effect (see [contributors](https://github.com/cloudevents/spec/blob/master/community/contributors.md))

## Programming Event Grid
* Batching
* Tip: [*ngrok*](https://ngrok.com/) is a useful tool during development and testing
* [Azure Event Grid event schema](https://docs.microsoft.com/en-us/azure/event-grid/event-schema)
  * General schema with metadata
  * Source-specific data (e.g. [storage](https://docs.microsoft.com/en-us/azure/event-grid/event-schema-blob-storage))
* Implement [endpoint validation](https://docs.microsoft.com/en-us/azure/event-grid/receive-events#endpoint-validation)
* [Event filtering](https://docs.microsoft.com/en-us/azure/event-grid/event-filtering)
* Manage events that could not be delivered using [Dead-lettering](https://docs.microsoft.com/en-us/azure/event-grid/manage-event-delivery)
  * Note [retry schedule](https://docs.microsoft.com/en-us/azure/event-grid/delivery-and-retry#retry-schedule-and-duration)

## Sources
* https://docs.microsoft.com/en-us/azure/event-grid/overview#event-handlers
* https://medium.com/microsoftazure/azure-event-grid-the-whole-story-4b7b4ec4ad23