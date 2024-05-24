using System.Security.Cryptography.X509Certificates;
using System.Text;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository saleRepository;
        private readonly IProductRepository productRepository;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository)
        {
            this.saleRepository = saleRepository;
            this.productRepository = productRepository;
        }

        public async Task<SaleDto> Add(SaleDto dto)
        {
            dto.ActivationCode = GenerateRandomString(8);
            dto.Date = DateOnly.FromDateTime(DateTime.Now);

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

        public async Task<List<SaleDto>> GetFromUser(int id)
        {
            return ListModelToDto(await saleRepository.GetFromUser(id));
        }

        public List<SaleDto> GetAll()
        {
            List<Sale> sales = saleRepository.GetAll();
            List<SaleDto> salesDtos = ListModelToDto(sales);
            return salesDtos;
        }

        public async Task<Boolean> HasBuy(HasBuy hasBuy) {
            List<Sale> sales = await this.saleRepository.GetFromUser(hasBuy.UserId);

            foreach (Sale sale in sales) {
                if (sale.ProductId == hasBuy.ProductId) {
                    return true;
                }
            }
            
            return false;
        }

        public async Task<List<SaleDto>> GetLast7Days()
        {
            return ListModelToDto(await this.saleRepository.GetLast7Days());
        }

        public async Task<float> GetTotalRevenuesFromLast7Days()
        {
            float totalRevenue = 0;
            List<Sale> sales = await this.saleRepository.GetLast7Days();

            sales.ForEach(async s => {
                Product product = await this.productRepository.Get(s.ProductId);
                totalRevenue += product.Price;
            });

            return totalRevenue;
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
                ActivationCode = saleDto.ActivationCode!,
                ProductId = saleDto.ProductId,
                UserId = saleDto.UserId,
                Date = saleDto.Date
            };

            return sale;
        }

        private string GenerateRandomString(int length)
        {
            char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();
            
            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(characters.Length);
                result.Append(characters[randomIndex]);
            }
            
            return result.ToString();
        }

    }
}