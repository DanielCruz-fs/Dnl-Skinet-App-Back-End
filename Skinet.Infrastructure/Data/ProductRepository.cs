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

        public async Task<IList<Product>> GetProductsAsync()
        {
            return await this.context.Products.Include(p => p.ProductBrand)
                                              .Include(p => p.ProductType)
                                              .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            /* 
             * FirstOrDefault vs SingleOrDefault vs Find
             * FirstOrDefault: will return an entity as soon as it finds in the list otherwise returns Null
             * SingleOrDefault: if it finds more than one of the same entity in the list, it will throw an exception
             * Find: it does not accept an Iqueryable, hence can not use eager loading
             */

            return await this.context.Products.Include(p => p.ProductBrand)
                                              .Include(p => p.ProductType)
                                              .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<ProductBrand>> GetProductBrandsAsync()
        {
            return await this.context.ProductBrands.ToListAsync();
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            return await this.context.ProductTypes.ToListAsync();
        }
    }
}
