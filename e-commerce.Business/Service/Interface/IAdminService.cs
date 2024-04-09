using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IAdminService
    {
        Task<AdminDto> Add(AdminDto dto);

        Task<AdminDto> Update(AdminDto dto);

        Task<int> Delete(string id);

        Task<AdminDto> Get(string id);

        List<AdminDto> GetAll();
    }
}