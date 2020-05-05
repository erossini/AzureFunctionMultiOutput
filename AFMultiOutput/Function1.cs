using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AFMultiOutput
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [ServiceBus("message-out1", Connection = "ServiceBusConnectionString1")] IAsyncCollector<dynamic> outputServiceBus1,
            [ServiceBus("message-out2", Connection = "ServiceBusConnectionString2")] IAsyncCollector<dynamic> outputServiceBus2,
            ILogger log)
        {
            await outputServiceBus1.AddAsync("Item1");
            await outputServiceBus2.AddAsync("Item2");

            return new OkObjectResult(null);
        }
    }
}
