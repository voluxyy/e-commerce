using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> Add(Admin admin);

        Task<Admin> Update(Admin admin);

        Task<int> Delete(int id);

        Task<Admin> Get(int id);

        List<Admin> GetAll();
    }
}