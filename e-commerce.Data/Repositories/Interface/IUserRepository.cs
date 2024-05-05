using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);

        Task<User> Update(User user);

        Task<int> Delete(int id);

        Task<User> Get(int id);

        List<User> GetAll();

        Task<User> GetByEmail(string email);
    }
}