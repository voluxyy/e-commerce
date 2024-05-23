using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface IReviewService
    {
        Task<ReviewDto> Add(ReviewDto dto);

        Task<ReviewDto> Update(ReviewDto dto);

        Task<int> Delete(int id);

        Task<ReviewDto> Get(int id);

        List<ReviewDto> GetAll();

        List<ReviewDto> GetFromProduct(int id);

        RateDto GetAverageRate(int id);
    }
}