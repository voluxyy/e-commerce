using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> Add(Admin admin);

        Task<Admin> Update(Admin admin);

        Task<int> Delete(string id);

        Task<Admin> Get(string id);

        List<Admin> GetAll();
    }
}