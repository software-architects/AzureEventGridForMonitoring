# Azure Functions

[doc](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)

## General

* [Serverless](https://azure.microsoft.com/en-us/overview/serverless-computing/) compute service
  * Handles provisioning, management, scalable
  * [Comparison of serverless](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs?toc=/azure/azure-functions/durable/toc.json)
* Run small pieces of code = functions
* Lots of languages supported C#, Java, JavaScript, Python, and PowerShell
* Hosting
  * Introduced `consumption plan`
    * completely managed + auto scale
    * [Pay-per-user](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview#pricing)
  * [(Elastic) Premium plan](https://docs.microsoft.com/en-us/azure/azure-functions/functions-scale#premium-plan)
  * [Dedicated (App Service) plan](https://docs.microsoft.com/en-us/azure/azure-functions/functions-scale#app-service-plan) 
* [Other features...](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview#features)
* [Durable Functions](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview?tabs=csharp) --> stateful functions
* [Functions vs. WebJobs](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs?toc=/azure/azure-functions/durable/toc.json#compare-functions-and-webjobs)
* Supports PowerShell --> Replacement for Automation Account?
* [Function proxies](https://docs.microsoft.com/en-us/azure/azure-functions/functions-proxies)

## Use Cases
* processing bulk data
* integrating systems
* building simple APIs
* micro-services architecture

# Function Host vs. Function App

## Triggers
* Cause a function to run
* Exactly one trigger per function
* Triggers can have payload (HTTP Post Body)
* No hardcoding
* Take a look a function.json

## Bindings
* Input or output bindings
* List of bindings see [here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings#supported-bindings)
* Declaratively connect function to other resource
* Binding data is passed as param to the function (e.g. event grid message)
* No hardcoding
* Take a look a function.json

## Security
* https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts
* [Security Center](https://azure.microsoft.com/en-us/services/security-center/)
* Azure Monitor
* Azure Sentinel
* Enforce HTTPS
* [Function access keys](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts#function-access-keys)
  * Function key: Only access function
  * Host keys: Access all functions of host (function app)
  * **!default!** key is host key
  * master key: allows administrative access
  * [system key](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts#system-key): used if Azure service wants to access function. E.g. EG subscription transparently created system key (see portal) 
* Prefer to use [MSI/AAD](https://docs.microsoft.com/en-us/azure/app-service/overview-authentication-authorization) to secure access to and from function
* For additional security see [here](https://docs.microsoft.com/en-us/azure/azure-functions/security-concepts)

## Developing with Functions
* [Function app](https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference#function-app)
  * Function app is the execution context 
  * holds multiple functions
  * deployment unit/scaled together
  * functions in app share pricing plan, runtime version, configuration