using System;
using System.Threading.Tasks;
using AzureItems.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureItems.ServiceBus.Worker
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([ServiceBusTrigger("todoitemqueue", Connection = "ServiceBusConnStr")] AzureItem azureItem,
            [CosmosDB(databaseName: "arndevDB", collectionName: "arndevContainer", ConnectionStringSetting = "TodoDBConnStr")] IAsyncCollector<dynamic> azureItemsCollector,
            ILogger log)
        {
            
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {azureItem}");
            await azureItemsCollector.AddAsync(azureItem);
        }
    }
}
