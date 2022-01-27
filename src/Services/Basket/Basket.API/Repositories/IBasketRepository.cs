using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetAsync(string userName);
        Task<ShoppingCart> UpdateAsync(ShoppingCart cart);
        Task DeleteAsync(string userName);
    }
}
