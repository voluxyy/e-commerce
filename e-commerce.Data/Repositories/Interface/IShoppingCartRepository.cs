using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> Add(ShoppingCart shoppingCart);

        Task<ShoppingCart> Update(ShoppingCart shoppingCart);

        Task<int> Delete(int id);

        Task<ShoppingCart> Get(int id);

        Task<ShoppingCart> GetFromUser(int id);

        List<ShoppingCart> GetAll();
    }
}