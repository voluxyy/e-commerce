using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> Add(Category category);
        Task<int> Delete(int id);
        Task<Category> Get(int id);
        List<Category> GetAll();
        Task<Category> Update(Category category);
    }
}