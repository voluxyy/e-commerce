using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IProductListService
    {
        Task<ProductListDto> Add(ProductListDto dto);

        Task<ProductListDto> Update(ProductListDto dto);

        Task<int> Delete(int id);
        Task<int> DeleteFromProduct(int id);

        Task<ProductListDto> Get(int id);

        Task<List<ProductListDto>> GetFromShoppingCart(int id);

        List<ProductListDto> GetAll();
    }
}