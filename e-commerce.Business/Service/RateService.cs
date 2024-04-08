using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class RateService : IRateService
    {
        private readonly IRateRepository rateRepository;

        public RateService(IRateRepository rateRepository)
        {
            this.rateRepository = rateRepository;
        }

        public async Task<RateDto> Add(RateDto dto)
        {
            Rate rate = DtoToModel(dto);
            await rateRepository.Add(rate);
            RateDto rateDto = ModelToDto(rate);

            return rateDto;
        }

        public async Task<RateDto> Update(RateDto dto)
        {
            Rate rate = DtoToModel(dto);
            await rateRepository.Update(rate);
            RateDto rateDto = ModelToDto(rate);

            return rateDto;
        }

        public async Task<int> Delete(int id)
        {
            return await rateRepository.Delete(id);
        }

        public async Task<RateDto> Get(int id)
        {
            return ModelToDto(await rateRepository.Get(id));
        }

        public List<RateDto> GetAll()
        {
            List<Rate> rates = rateRepository.GetAll();
            List<RateDto> ratesDtos = ListModelToDto(rates);
            return ratesDtos;
        }

        private List<RateDto> ListModelToDto(List<Rate> rates)
        {
            List<RateDto> ratesDtos = new List<RateDto>();
            foreach (Rate rate in rates)
            {
                ratesDtos.Add(ModelToDto(rate));
            }
            return ratesDtos;
        }

        private RateDto ModelToDto(Rate rate)
        {
            RateDto rateDto = new RateDto
            {
                Id = rate.Id,
                Value = rate.Value,
                ProductId = rate.ProductId,
                UserId = rate.UserId
            };

            return rateDto;
        }

        private Rate DtoToModel(RateDto rateDto)
        {
            Rate rate = new Rate
            {
                Id = rateDto.Id,
                Value = rateDto.Value,
                ProductId = rateDto.ProductId,
                UserId = rateDto.UserId
            };

            return rate;
        }
    }
}