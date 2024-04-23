using ecommerce.Business.Dto;

namespace ecommerce.Business.Service.Interface
{
    public interface IWishlistService
    {
        Task<WishlistDto> Add(WishlistDto dto);
        Task<int> Delete(int id);
        Task<WishlistDto> Get(int id);
        List<WishlistDto> GetAll();
        Task<WishlistDto> Update(WishlistDto dto);
    }
}