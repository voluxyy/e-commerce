using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> Add(ShoppingCartDto dto);

        Task<ShoppingCartDto> Update(ShoppingCartDto dto);

        Task<int> Delete(int id);

        Task<ShoppingCartDto> Get(int id);

        List<ShoppingCartDto> GetAll();
    }
}