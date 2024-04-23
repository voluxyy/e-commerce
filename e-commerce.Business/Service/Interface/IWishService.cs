using ecommerce.Business.Dto;

namespace ecommerce.Business.Service.Interface
{
    public interface IWishService
    {
        Task<WishDto> Add(WishDto dto);
        Task<int> Delete(int id);
        Task<WishDto> Get(int id);
        List<WishDto> GetAll();
        Task<WishDto> Update(WishDto dto);
    }
}