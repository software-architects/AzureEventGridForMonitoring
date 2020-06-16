# Azure Logic Apps


* Serverless workflow engine in Azure
* Allows to integrate other services as input and output = [connectors](https://docs.microsoft.com/en-us/azure/connectors/apis-list)
* Visual designer to design processes & workflow
  * readable Json format
* Triggers
  * Event-based trigger
  * Timer Trigger (Recurring)

## Why use Logic Apps
* Visual designer (usable by domain experts), often no code necessary
* Connect legacy apps with cloud
* Large number of [connectors](https://docs.microsoft.com/en-us/azure/connectors/apis-list)
* Event grid connector
* hybrid solutions (integrate on-prem systems e.g. EG custom topics)
* Support MSI

## Concepts

* Workflow: Visualize, design, build, automate, and deploy business processes as series of steps.
* Managed connectors: Your logic apps need access to data, services, and systems. You can use prebuilt Microsoft-managed connectors that are designed to connect, access, and work with your data. See Connectors for Azure Logic Apps.
* Triggers: Many Microsoft-managed connectors provide triggers that fire when events or new data meet specified conditions. For example, an event might be getting an email or detecting changes in your Azure Storage account. Each time the trigger fires, the Logic Apps engine creates a new logic app instance that runs the workflow.
* Actions: Actions are all the steps that happen after the trigger. Each action usually maps to an operation that's defined by a managed connector, custom API, or custom connector.

## Advanced Scenarios
* Access secured resources within VPN with [ISE](https://docs.microsoft.com/en-us/azure/logic-apps/connect-virtual-network-vnet-isolated-environment-overview)

https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs?toc=/azure/azure-functions/durable/toc.json