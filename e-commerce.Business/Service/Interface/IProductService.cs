using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IProductService
    {
        Task<ProductDto> Add(ProductDto dto, byte[] imageFile);

        Task<ProductDto> Update(ProductDto dto);

        Task<int> Delete(int id);

        Task<ProductDto> Get(int id);

        List<ProductDto> GetAll();
    }
}