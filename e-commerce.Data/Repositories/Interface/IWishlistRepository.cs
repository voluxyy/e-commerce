using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories.Interface
{
    public interface IWishlistRepository
    {
        Task<Wishlist> Add(Wishlist wishlist);
        Task<int> Delete(int id);
        Task<Wishlist> Get(int id);
        List<Wishlist> GetAll();
        Task<Wishlist> Update(Wishlist wishlist);
    }
}