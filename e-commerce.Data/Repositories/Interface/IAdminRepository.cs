using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> Add(Admin admin);

        Task<Admin> Update(Admin admin);

        Task<int> Delete(Guid id);

        Task<Admin> Get(Guid id);

        List<Admin> GetAll();
    }
}