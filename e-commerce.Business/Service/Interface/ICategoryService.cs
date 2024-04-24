using ecommerce.Business.Dto;

namespace ecommerce.Business.Service.Interface
{
    public interface ICategoryService
    {
        Task<CategoryDto> Add(CategoryDto dto);
        Task<int> Delete(int id);
        Task<CategoryDto> Get(int id);
        List<CategoryDto> GetAll();
        Task<CategoryDto> Update(CategoryDto dto);
    }
}