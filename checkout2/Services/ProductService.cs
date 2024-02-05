using checkout2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkout2.Services
{
    public class ProductService: IProductService
    {
        private readonly NoSqlDataBase<ProductDTO> _context;
        public string container = "Products";

        public ProductService()
        {
            _context = new();
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            return await _context.GetAllItens(container);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(string category)
        {
            return await _context.GetByPredicate(container,m => m.Category == category);
        }

        public async Task AddProduct(ProductDTO product)
        {
            await _context.Add(container,product,product.Category);
        }
        public async Task UpdateProduct(ProductDTO product)
        {
            await _context.Update(container, product, product.Category);
        }
    }
}
