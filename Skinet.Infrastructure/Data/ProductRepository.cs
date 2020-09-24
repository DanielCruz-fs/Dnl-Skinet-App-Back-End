using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await this.context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            return await this.context.Products.ToListAsync();
        }
    }
}
