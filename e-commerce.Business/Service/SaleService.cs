using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            this.saleRepository = saleRepository;
        }

        public async Task<SaleDto> Add(SaleDto dto)
        {
            Sale sale = DtoToModel(dto);
            await saleRepository.Add(sale);
            SaleDto saleDto = ModelToDto(sale);

            return saleDto;
        }

        public async Task<SaleDto> Update(SaleDto dto)
        {
            Sale sale = DtoToModel(dto);
            await saleRepository.Update(sale);
            SaleDto saleDto = ModelToDto(sale);

            return saleDto;
        }

        public async Task<int> Delete(int id)
        {
            return await saleRepository.Delete(id);
        }

        public async Task<SaleDto> Get(int id)
        {
            return ModelToDto(await saleRepository.Get(id));
        }

        public List<SaleDto> GetAll()
        {
            List<Sale> sales = saleRepository.GetAll();
            List<SaleDto> salesDtos = ListModelToDto(sales);
            return salesDtos;
        }

        private List<SaleDto> ListModelToDto(List<Sale> sales)
        {
            List<SaleDto> salesDtos = new List<SaleDto>();
            foreach (Sale sale in sales)
            {
                salesDtos.Add(ModelToDto(sale));
            }
            return salesDtos;
        }

        private SaleDto ModelToDto(Sale sale)
        {
            SaleDto saleDto = new SaleDto
            {
                Id = sale.Id,
                ActivationCode = sale.ActivationCode,
                ProductId = sale.ProductId,
                UserId = sale.UserId,
                Date = sale.Date
            };

            return saleDto;
        }

        private Sale DtoToModel(SaleDto saleDto)
        {
            Sale sale = new Sale
            {
                Id = saleDto.Id,
                ActivationCode = saleDto.ActivationCode,
                ProductId = saleDto.ProductId,
                UserId = saleDto.UserId,
                Date = saleDto.Date
            };

            return sale;
        }
    }
}