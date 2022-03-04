using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderGenerator
{
    public static class PersistOrder
    {
        [FunctionName("PersistOrder")]
        public static void Run([ServiceBusTrigger("orders", Connection = "ServiceBusConnection")]string myQueueItem,
            [CosmosDB("Orders", "orders", Connection = "CosmosDbConnection")] out dynamic document,
            
            ILogger log)
        {
            try
            {

                document = JsonConvert.SerializeObject(myQueueItem);

                log.LogInformation("C# Service Bus Trigger inserted one row.");
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }
        }
    }
}
