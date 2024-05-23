using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<ReviewDto> Add(ReviewDto dto)
        {
            Review review = DtoToModel(dto);
            await reviewRepository.Add(review);
            ReviewDto reviewDto = ModelToDto(review);

            return reviewDto;
        }

        public async Task<ReviewDto> Update(ReviewDto dto)
        {
            Review review = DtoToModel(dto);
            await reviewRepository.Update(review);
            ReviewDto reviewDto = ModelToDto(review);

            return reviewDto;
        }

        public async Task<int> Delete(int id)
        {
            return await reviewRepository.Delete(id);
        }

        public async Task<ReviewDto> Get(int id)
        {
            return ModelToDto(await reviewRepository.Get(id));
        }

        public List<ReviewDto> GetAll()
        {
            List<Review> reviews = reviewRepository.GetAll();
            List<ReviewDto> reviewsDtos = ListModelToDto(reviews);
            return reviewsDtos;
        }

        public List<ReviewDto> GetFromProduct(int id)
        {
            List<Review> reviews = reviewRepository.GetFromProduct(id);
            List<ReviewDto> reviewDtos = ListModelToDto(reviews);
            return reviewDtos;
        }

        public RateDto GetAverageRate(int id)
        {
            List<Review> reviews = reviewRepository.GetFromProduct(id);
            
            if (reviews == null || !reviews.Any())
            {
                return new RateDto { Value = 0 };
            }

            double averageRate = reviews.Average(review => review.Rate);

            string formattedAverageRate = averageRate.ToString("0.00");

            double formattedRate;
            if (double.TryParse(formattedAverageRate, out formattedRate))
            {
                RateDto rateDto = new RateDto { Value = formattedRate };
                return rateDto;
            }
            else
            {
                return new RateDto { Value = 0 };
            }
        }

        private List<ReviewDto> ListModelToDto(List<Review> reviews)
        {
            List<ReviewDto> reviewsDtos = new List<ReviewDto>();
            foreach (Review review in reviews)
            {
                reviewsDtos.Add(ModelToDto(review));
            }
            return reviewsDtos;
        }

        private ReviewDto ModelToDto(Review review)
        {
            ReviewDto reviewDto = new ReviewDto
            {
                Id = review.Id,
                Title = review.Title,
                Description = review.Description,
                Rate = review.Rate,
                ProductId = review.ProductId,
                UserId = review.UserId
            };

            return reviewDto;
        }

        private Review DtoToModel(ReviewDto reviewDto)
        {
            Review review = new Review
            {
                Id = reviewDto.Id,
                Title = reviewDto.Title,
                Description = reviewDto.Description,
                Rate = reviewDto.Rate,
                ProductId = reviewDto.ProductId,
                UserId = reviewDto.UserId
            };

            return review;
        }
    }
}