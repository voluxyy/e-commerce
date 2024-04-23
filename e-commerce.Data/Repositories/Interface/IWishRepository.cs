using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories.Interface
{
    public interface IWishRepository
    {
        Task<Wish> Add(Wish wish);
        Task<int> Delete(int id);
        Task<Wish> Get(int id);
        List<Wish> GetAll();
        Task<Wish> Update(Wish wish);
    }
}