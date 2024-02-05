using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace checkout2
{

    public class ProductDTO
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("Reference")]
        public string Reference { get; set; }
        [JsonPropertyName("Desc")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }
        public string Category { get; set; }
        public void SetAsDeleted()
        {
            this.Deleted = true;
        }
    }

    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Deleted { get; set; }
        public string Category { get; set; }
        public void SetAsDeleted()
        {
            this.Deleted = true;
        }
    }
    public class ProductBinding
    {
        public HttpResponse HttpResponse {  get; set; }
        public ProductDTO Product { get; set; }
    }
}
