# Azure Storage
[doc](https://docs.microsoft.com/en-us/azure/storage/)

## Types

* [Blobs](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)
  * Massively scalable object store for text and binary data
  * Used for:
    * Serving images or documents directly to a browser.
    * Storing files for distributed access.
    * Streaming video and audio
    * Storing data for backup and restore, disaster recovery, and archiving
    * ...
* [Files](https://docs.microsoft.com/en-us/azure/storage/files/storage-files-introduction)
  * SMB file shares as a service
  * Especially relevant for existing apps that rely on SMB
* [Queues](https://docs.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction)
  * For really *large* queues
  * Consider [Service Bus](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview) as an alternative ([how to choose](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-azure-and-service-bus-queues-compared-contrasted))
* [Tables](https://docs.microsoft.com/en-us/azure/storage/tables/table-storage-overview)
  * Not very relevant anymore, consider [CosmosDB](https://docs.microsoft.com/en-us/azure/cosmos-db/table-introduction) instead
* [Disks](https://docs.microsoft.com/en-us/azure/virtual-machines/windows/managed-disks-overview)
  * Disks for VMs

## Storage Account

[doc](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview?toc=/azure/storage/blobs/toc.json)

* *http://<storage-account-name>.blob.core.windows.net*
  * Be careful with naming concept! Storage account names must be globally unique and can *only contain numbers and lowercase letters* (e.g. no dashes or underscores; historical reasons)
* Recommended [type](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview?toc=/azure/storage/blobs/toc.json#types-of-storage-accounts): *General-purpose v2*
* [*Hot*, *cool*, *archive*](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview?toc=/azure/storage/blobs/toc.json#access-tiers-for-block-blob-data)
  * Keep [costs](https://azure.microsoft.com/en-us/pricing/details/storage/blobs/) in mind
  * Avoid archive if not absolutely necessary
  * Consider setting up [storage lifecycle](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-lifecycle-management-concepts?tabs=azure-portal)
* [*Standard* or *Premium* performance tiers](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-performance-tiers)
* ARM References
  * [Storage accounts](https://docs.microsoft.com/en-us/azure/templates/microsoft.storage/2019-06-01/storageaccounts)
  * [Blob storage](https://docs.microsoft.com/en-us/azure/templates/microsoft.storage/2019-06-01/storageaccounts/blobservices)
  * [Containers](https://docs.microsoft.com/en-us/azure/templates/microsoft.storage/2019-06-01/storageaccounts/blobservices/containers)

> [Sample for creating storage account with ARM template](azuredeploy-storage.json)

## Blob Storage

![Blob Storage Structure](https://docs.microsoft.com/en-us/azure/storage/blobs/media/storage-blobs-introduction/blob1.png)

### Tools

* [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)
* [Data movement tools](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction#move-data-to-blob-storage)

### Security and Compliance Features

* **Limit anonymous access**, enable only if absolutely necessary (e.g. [static website hosting](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-static-website))
  * default is private
  * every request must be authorized (SAS/accountkey/MSI)
* Choose proper [level of redundancy](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview?toc=/azure/storage/blobs/toc.json#redundancy)
  * Recommendation for default level if no specific requirements are given: *[Zone](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview?toc=/azure/storage/blobs/toc.json#redundancy)-redundant storage* ([docs](https://docs.microsoft.com/en-us/azure/storage/common/storage-redundancy?toc=/azure/storage/blobs/toc.json#zone-redundant-storage))
* [Immutability option](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-immutable-storage) WORM
* Always encrypted, [MS-managed or customer-managed keys](https://docs.microsoft.com/en-us/azure/storage/common/storage-service-encryption)
* [Secure transfer](https://docs.microsoft.com/en-us/azure/storage/common/storage-require-secure-transfer) (*HTTPS*) should be required
* [*Advanced threat protection*](https://docs.microsoft.com/en-us/azure/storage/common/storage-advanced-threat-protection?tabs=azure-portal) is available
  * detects unusual and potentially harmful attempts to access or exploit storage accounts
  * forwarded to Azure Security Center
  * Log Analytics connector
* [*Soft delete*](https://docs.microsoft.com/en-us/azure/storage/blobs/soft-delete-overview) is available based on [blob snapshots](https://docs.microsoft.com/en-us/azure/storage/blobs/snapshots-overview) (alternative: [Blob versioning](https://docs.microsoft.com/en-us/azure/storage/blobs/versioning-overview?tabs=powershell), currently in preview)
* Limit access using [*shared access signatures*](https://docs.microsoft.com/en-us/azure/storage/common/storage-sas-overview)
  * Ad hoc SAS
  * Policy SAS
  * [Best practices](detects unusual and potentially harmful attempts to access or exploit storage accounts)
* Use Azure AD to secure access to blob storage (in particular [managed identities](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview) are useful for that)
  * If not possible, store access secrets in [key vault](https://docs.microsoft.com/en-us/azure/key-vault/general/overview) or at least protect them properly (e.g. ASP.NET Core [Data Protection](https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-3.1))
* Limit access using [IP firewall rules](https://docs.microsoft.com/en-us/azure/storage/common/storage-network-security#grant-access-from-an-internet-ip-range)
* Use [*private endpoints*](https://docs.microsoft.com/en-us/azure/private-link/create-private-endpoint-storage-portal) to limit access to certain virtual networks
* [Enable logging](https://docs.microsoft.com/en-us/azure/storage/common/storage-analytics-logging?tabs=dotnet) and/or [monitoring](https://docs.microsoft.com/en-us/azure/storage/common/monitor-storage) for detailed access tracking

### Concurrency

* [*Optimistic Concurrency*](https://docs.microsoft.com/en-us/azure/storage/common/storage-concurrency?toc=/azure/storage/blobs/toc.json#optimistic-concurrency-for-blobs-and-containers)
  * Based on *HTTP ETag* and *If-Match* headers
* [*Pessimistic Concurrency*](https://docs.microsoft.com/en-us/azure/storage/common/storage-concurrency?toc=/azure/storage/blobs/toc.json#pessimistic-concurrency-for-blobs)
  * Based on [Blob *Leases*](https://docs.microsoft.com/en-us/rest/api/storageservices/Lease-Blob)

### Performance Considerations

* *Premium* performance only available for *BlockBlobStorage*, limited for *General-purpose V2* ([details](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview#types-of-storage-accounts))
* [Batching](https://docs.microsoft.com/en-us/dotnet/api/azure.storage.blobs.specialized.blobbatchclient)
  * [REST reference](https://docs.microsoft.com/en-us/rest/api/storageservices/blob-batch)
* [Append blobs](https://docs.microsoft.com/en-us/rest/api/storageservices/understanding-block-blobs--append-blobs--and-page-blobs#about-append-blobs)
* [Copy blob from URL](https://docs.microsoft.com/en-us/rest/api/storageservices/copy-blob-from-url) to move/copy existing blobs
* [Build blobs incrementally](https://docs.microsoft.com/en-us/rest/api/storageservices/put-block-list)
* Use [Event Grid integration](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-event-overview) to prevent polling
* Use [*AzCopy*](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azcopy-v10) tool to transfer larger block blobs

### Samples

* [.NET Storage Client Library](https://docs.microsoft.com/en-us/dotnet/api/overview/azure/storage?view=azure-dotnet)
* Code Samples see *How to* section in [blob storage docs](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)
* Samples on GitHub
  * [Upload and download of blobs](https://github.com/Azure/azure-sdk-for-net/blob/master/sdk/storage/Azure.Storage.Blobs/samples/Sample01b_HelloWorldAsync.cs)
  * [Authentication](https://github.com/Azure/azure-sdk-for-net/blob/master/sdk/storage/Azure.Storage.Blobs/samples/Sample02_Auth.cs)
* [Tests on GitHub](https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/storage/Azure.Storage.Blobs/tests)

### Upcoming Features (Preview)

* [Blob versioning](https://docs.microsoft.com/en-us/azure/storage/blobs/versioning-overview?tabs=powershell)
* [Change feeds](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-change-feed?tabs=azure-portal)
* [Point-in-time restore](https://docs.microsoft.com/en-us/azure/storage/blobs/point-in-time-restore-overview)
* [Blob index tags](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-manage-find-blobs)
