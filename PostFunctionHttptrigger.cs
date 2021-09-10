using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using BasicFunction.Model;

namespace BasicFunction
{
    public static class PostFunctionHttptrigger
    {
        [FunctionName("PostFunctionHttptrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethods.Post), Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = default;
            var person = await req.Content.ReadAsAsync<Person>();

            ObjectResult result;
            string responseMessage = default;
            name = person != null ? person.Name : name;

            if (string.IsNullOrEmpty(name))
            {
                responseMessage = "Please provide a value on the body, be a gentleman!! =)";
                result = new BadRequestObjectResult(responseMessage);
            }
            else
            {
                responseMessage = $"Hey there {name} from body! Salute!! =)";
                result = new OkObjectResult(responseMessage);
            }

            return result;
        }
    }
}
