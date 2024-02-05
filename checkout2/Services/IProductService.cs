using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkout2.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(string category);
        Task AddProduct(ProductDTO product);
        Task UpdateProduct(ProductDTO product);
    }
}
