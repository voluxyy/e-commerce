using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories.Interface
{
    public interface IWishListRepository
    {
        Task<WishList> Add(WishList wishList);
        Task<int> Delete(int id);
        Task<WishList> Get(int id);
        List<WishList> GetAll();
        Task<WishList> Update(WishList wishList);
    }
}