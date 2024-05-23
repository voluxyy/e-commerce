using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IUserService
    {
        Task<UserDto> Add(UserDto dto);

        Task<UserDto> Update(UserDto dto);

        Task<UserDto> UpdatePassword(UserDto dto);

        Task<int> Delete(int id);

        Task<UserDto> Get(int id);

        List<UserDto> GetAll();

        Task<UserDto> CheckConnection(LoginDto dto);

        Task<UserDto> UpdateMoney(MoneyDto dto);
    }
}