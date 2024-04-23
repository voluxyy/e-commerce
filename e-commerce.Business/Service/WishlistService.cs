using ecommerce.Business.Service.Interface;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories.Interface;

namespace ecommerce.Business.Service
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        public async Task<WishlistDto> Add(WishlistDto dto)
        {
            Wishlist wishlist = DtoToModel(dto);
            await wishlistRepository.Add(wishlist);
            WishlistDto wishlistDto = ModelToDto(wishlist);

            return wishlistDto;
        }

        public async Task<WishlistDto> Update(WishlistDto dto)
        {
            Wishlist wishlist = DtoToModel(dto);
            await wishlistRepository.Update(wishlist);
            WishlistDto wishlistDto = ModelToDto(wishlist);

            return wishlistDto;
        }

        public async Task<int> Delete(int id)
        {
            return await wishlistRepository.Delete(id);
        }

        public async Task<WishlistDto> Get(int id)
        {
            return ModelToDto(await wishlistRepository.Get(id));
        }

        public List<WishlistDto> GetAll()
        {
            List<Wishlist> wishlists = wishlistRepository.GetAll();
            List<WishlistDto> wishlistDtos = ListModelToDto(wishlists);
            return wishlistDtos;
        }

        private List<WishlistDto> ListModelToDto(List<Wishlist> wishlists)
        {
            List<WishlistDto> wishlistDtos = new List<WishlistDto>();
            foreach (Wishlist wishlist in wishlists)
            {
                wishlistDtos.Add(ModelToDto(wishlist));
            }
            return wishlistDtos;
        }

        private WishlistDto ModelToDto(Wishlist wishlist)
        {
            WishlistDto wishlistDto = new WishlistDto
            {
                Id = wishlist.Id,
                UserId = wishlist.UserId,
            };

            return wishlistDto;
        }

        private Wishlist DtoToModel(WishlistDto wishlistDto)
        {
            Wishlist wishlist = new Wishlist
            {
                Id = wishlistDto.Id,
                UserId = wishlistDto.UserId,
            };

            return wishlist;
        }
    }
}
