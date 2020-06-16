# Azure Resource Manager

## Concepts

![ARM Concept](https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/media/overview/consistent-management-layer.png)

* Carefully define naming concept upfront ([Microsoft's tips](https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/naming-and-tagging))
* List of all [Azure Resource Providers](https://docs.microsoft.com/en-us/azure/azure-resource-manager/management/azure-services-resource-providers)
* [ARM template reference](https://docs.microsoft.com/en-us/azure/templates/microsoft.resources/2019-10-01/resourcegroups) for resource groups

## ARM Templates

* Automate deployments
* Use the practice of *infrastructure as code*
* Repeatable
* Idempotent
* Declarative language using JSON
  * Powerful when done, hard to develop and debug
  * Feature-rich [function library](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/template-functions)
* [*What if* feature](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/template-deploy-what-if?tabs=azure-powershell) (currently in preview)
* [Docs](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/overview)
* Often combined with or sometimes replaced by:
  * [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/what-is-azure-cli?view=azure-cli-latest)
  * [Azure PowerShell](https://docs.microsoft.com/en-us/powershell/azure/?view=azps-3.8.0)
  * [Azure Library for .NET](https://github.com/Azure/azure-libraries-for-net)

> [Sample for creating resource group with ARM template](provisioning/deploy.json)
