using ecommerce.Business.Dto;

namespace ecommerce.Business.Service.Interface
{
    public interface IWishListService
    {
        Task<WishListDto> Add(WishListDto dto);
        Task<int> Delete(int id);
        Task<WishListDto> Get(int id);
        List<WishListDto> GetAll();
        Task<WishListDto> Update(WishListDto dto);
    }
}