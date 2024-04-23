using ecommerce.Business.Service.Interface;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories.Interface;

namespace ecommerce.Business.Service
{
    public class WishService : IWishService
    {
        private readonly IWishRepository wishRepository;

        public WishService(IWishRepository wishRepository)
        {
            this.wishRepository = wishRepository;
        }

        public async Task<WishDto> Add(WishDto dto)
        {
            Wish wish = DtoToModel(dto);
            await wishRepository.Add(wish);
            WishDto wishDto = ModelToDto(wish);

            return wishDto;
        }

        public async Task<WishDto> Update(WishDto dto)
        {
            Wish wish = DtoToModel(dto);
            await wishRepository.Update(wish);
            WishDto wishDto = ModelToDto(wish);

            return wishDto;
        }

        public async Task<int> Delete(int id)
        {
            return await wishRepository.Delete(id);
        }

        public async Task<WishDto> Get(int id)
        {
            return ModelToDto(await wishRepository.Get(id));
        }

        public List<WishDto> GetAll()
        {
            List<Wish> wishs = wishRepository.GetAll();
            List<WishDto> wishDtos = ListModelToDto(wishs);
            return wishDtos;
        }

        private List<WishDto> ListModelToDto(List<Wish> wishs)
        {
            List<WishDto> wishDtos = new List<WishDto>();
            foreach (Wish wish in wishs)
            {
                wishDtos.Add(ModelToDto(wish));
            }
            return wishDtos;
        }

        private WishDto ModelToDto(Wish wish)
        {
            WishDto wishDto = new WishDto
            {
                Id = wish.Id,
                ProductId = wish.ProductId,
                UserId = wish.UserId,
            };

            return wishDto;
        }

        private Wish DtoToModel(WishDto wishDto)
        {
            Wish wish = new Wish
            {
                Id = wishDto.Id,
                ProductId = wishDto.ProductId,
                UserId = wishDto.UserId,
            };

            return wish;
        }
    }
}
