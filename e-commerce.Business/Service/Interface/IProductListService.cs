using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IProductListService
    {
        Task<ProductListDto> Add(ProductListDto dto);

        Task<ProductListDto> Update(ProductListDto dto);

        Task<int> Delete(int id);

        Task<ProductListDto> Get(int id);

        List<ProductListDto> GetAll();
    }
}