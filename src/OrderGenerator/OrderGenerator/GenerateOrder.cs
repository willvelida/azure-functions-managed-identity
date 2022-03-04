using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderGenerator
{
    public static class GenerateOrder
    {
        [FunctionName("GenerateOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order")] HttpRequest req,
            [ServiceBus("orders", Connection = "ServiceBusConnection")] IAsyncCollector<Order> collector,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                Order order = new Order { OrderId = Guid.NewGuid().ToString(), Item = "Cheese", Price = 5.99 };

                await collector.AddAsync(order);

                log.LogInformation($"Order with ID: {order.OrderId} processed");

                return new OkObjectResult(order);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }
        }
    }
}
