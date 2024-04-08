using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);

        Task<Product> Update(Product product);

        Task<int> Delete(int id);

        Task<Product> Get(int id);

        List<Product> GetAll();
    }
}