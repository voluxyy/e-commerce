using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IAdminService
    {
        Task<AdminDto> Add(AdminDto dto);

        Task<AdminDto> Update(AdminDto dto);

        Task<int> Delete(int id);

        Task<AdminDto> Get(int id);

        List<AdminDto> GetAll();
    }
}