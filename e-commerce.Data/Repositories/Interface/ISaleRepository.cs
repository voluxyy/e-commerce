using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> Add(Sale sale);

        Task<Sale> Update(Sale sale);

        Task<int> Delete(int id);

        Task<Sale> Get(int id);

        List<Sale> GetAll();

        Task<List<Sale>> GetFromUser(int id);

        Task<List<Sale>> GetLast7Days();
    }
}