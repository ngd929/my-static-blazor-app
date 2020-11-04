//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

//namespace Api
//{
//    public static class ProductsGet
//    {
//        [FunctionName("ProductsGet")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            log.LogInformation("C# HTTP trigger function processed a request.");

//            string name = req.Query["name"];

//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;

//            string responseMessage = string.IsNullOrEmpty(name)
//                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//                : $"Hello, {name}. This HTTP triggered function executed successfully.";

//            return new OkObjectResult(responseMessage);
//        }
//    }
//}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace Api
{
    public class ProductsGet
    {
        private readonly IProductData productData;

        public ProductsGet(IProductData productData)
        {
            this.productData = productData;
        }

        [FunctionName("ProductsGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req)
        {
            var products = await productData.GetProducts();
            return new OkObjectResult(products);
        }
    }
}