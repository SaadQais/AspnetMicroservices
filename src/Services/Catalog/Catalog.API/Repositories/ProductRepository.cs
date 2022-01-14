using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await (await _context.Products.FindAsync(p => true))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);

            return await (await _context.Products.FindAsync(filter))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await (await _context.Products.FindAsync(filter))
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await (await _context.Products.FindAsync(p => p.Id == id))
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Products.DeleteOneAsync(p => p.Id == id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
