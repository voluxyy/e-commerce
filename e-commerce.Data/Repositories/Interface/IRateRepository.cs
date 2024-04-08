using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IRateRepository
    {
        Task<Rate> Add(Rate rate);

        Task<Rate> Update(Rate rate);

        Task<int> Delete(int id);

        Task<Rate> Get(int id);

        List<Rate> GetAll();
    }
}