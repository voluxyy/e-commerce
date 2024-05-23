using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IProductService
    {

        Task<ProductDto> Add(ProductDto dto, byte[] imageFile);

        Task<ProductDto> Update(ProductDto dto, byte[] imageData);

        Task<int> Delete(int id);

        Task<ProductDto> Get(int id);

        List<ProductDto> GetAll();

        List<ProductDto> SearchBar(string searchItems);
    }
}