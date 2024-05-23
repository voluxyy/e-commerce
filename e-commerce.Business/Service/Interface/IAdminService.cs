using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IAdminService
    {
        Task<AdminDto> Add(AdminDto dto);

        Task<AdminDto> Update(AdminDto dto);
        
        Task<AdminDto> UpdatePassword(AdminDto dto);

        Task<int> Delete(Guid id);

        Task<AdminDto> Get(Guid id);

        List<AdminDto> GetAll();

        Task<AdminDto> CheckConnection(LoginDto dto);
    }
}