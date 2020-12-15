using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Core.Contracts;
using Bakery.Core.DTOs;
using Bakery.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Persistence
{
  public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Products.CountAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Product> products)
        {
            await _dbContext.Products.AddRangeAsync(products);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _dbContext.Products
                .Include(product => product.OrderItems)
                .OrderBy(product => product.Name)
                .Select(product => new ProductDto(product))
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<Product> GetAsync(int productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }

        public async Task DeleteAsync(ProductDto product)
        {
            Product dbProduct = await GetAsync(product.Id);

            if(dbProduct == null)
            {
                return;
            }

            _dbContext.Products.Remove(dbProduct);
        }
    }
}
