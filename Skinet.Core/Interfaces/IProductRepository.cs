using Skinet.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyCollection<Product>> GetProductsAsync();
    }
}
