using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);
        Task<Product> GetByIdAsync(string id);
        Task CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}
