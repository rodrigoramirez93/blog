using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace FunctionApp
{
    public class AzureFunction
    {
        private readonly AppSettings AppSettings;
        public AzureFunction(IOptions<AppSettings> appsettings)
        {
            AppSettings = appsettings.Value;
        }

        [FunctionName("AzureFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = AppSettings.SomeValue;
            return new OkObjectResult(result);
        }
    }
}
