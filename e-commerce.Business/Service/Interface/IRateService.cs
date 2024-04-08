using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IRateService
    {
        Task<RateDto> Add(RateDto dto);

        Task<RateDto> Update(RateDto dto);

        Task<int> Delete(int id);

        Task<RateDto> Get(int id);

        List<RateDto> GetAll();
    }
}