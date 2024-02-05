using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using checkout2.Services;

namespace checkout2
{
    public class checkout
    {
        private readonly IProductService _context;

        public checkout(IProductService context)
        {
            _context = context;
        }

        [FunctionName("Products")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", "post", "get", "delete", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            ProductDTO data = JsonConvert.DeserializeObject<ProductDTO>(requestBody);
                       
            string responseMessage = "opção Invalida";

            if (req.Method == "GET") {
                var list = await _context.GetProducts();
                responseMessage = JsonConvert.SerializeObject(list);
            }
            if (req.Method == "POST")
            {
                data.id = Guid.NewGuid().ToString();
                await _context.AddProduct(data);
                responseMessage = "Criado com sucesso";
            }
            if (req.Method == "PUT") {
                await _context.UpdateProduct(data);
                responseMessage = "Atualizado com sucesso";
            }
            if (req.Method == "DELETE") {  }
            return new OkObjectResult(responseMessage);
        }
    }
}
