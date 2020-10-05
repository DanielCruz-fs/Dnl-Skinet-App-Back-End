using Skinet.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IList<Product>> GetProductsAsync();
        Task<IList<ProductBrand>> GetProductBrandsAsync();
        Task<IList<ProductType>> GetProductTypesAsync();
    }
}
