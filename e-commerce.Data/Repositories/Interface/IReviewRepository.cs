using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> Add(Review review);

        Task<Review> Update(Review review);

        Task<int> Delete(int id);

        Task<Review> Get(int id);

        List<Review> GetAll();

        List<Review> GetFromProduct(int id);
    }
}