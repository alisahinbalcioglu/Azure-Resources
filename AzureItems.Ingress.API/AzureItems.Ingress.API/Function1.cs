using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureItems.Common;

namespace AzureItems.Ingress.API
{
    public static class Function1
    {
        [FunctionName("CreateAzure")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/CreateAzure")] HttpRequest req,
            [ServiceBus(queueOrTopicName:"todoitemqueue", Connection ="ServiceBusConnStr")] IAsyncCollector<dynamic> serviceBusCollector,
            ILogger log)

        {
            var bodyJson = await req.ReadAsStringAsync();
            var azureItem=System.Text.Json.JsonSerializer.Deserialize<AzureItem>(bodyJson);

            await serviceBusCollector.AddAsync(azureItem);

            
            return new OkObjectResult("Azure Item successfully added to servicebus");
        }
    }
}
