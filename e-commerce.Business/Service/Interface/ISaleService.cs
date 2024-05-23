using ecommerce.Business.Dto;

namespace ecommerce.Business.Service
{
    public interface ISaleService
    {
        Task<SaleDto> Add(SaleDto dto);

        Task<SaleDto> Update(SaleDto dto);

        Task<int> Delete(int id);

        Task<SaleDto> Get(int id);

        Task<List<SaleDto>> GetFromUser(int id);

        List<SaleDto> GetAll();
        
        Task<Boolean> HasBuy(HasBuy hasBuy);
    }
}