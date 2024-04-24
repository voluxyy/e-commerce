using ecommerce.Business.Service.Interface;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories.Interface;

namespace ecommerce.Business.Service
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository wishListRepository;

        public WishListService(IWishListRepository wishListRepository)
        {
            this.wishListRepository = wishListRepository;
        }

        public async Task<WishListDto> Add(WishListDto dto)
        {
            WishList wishList = DtoToModel(dto);
            await wishListRepository.Add(wishList);
            WishListDto wishListDto = ModelToDto(wishList);

            return wishListDto;
        }

        public async Task<WishListDto> Update(WishListDto dto)
        {
            WishList wishList = DtoToModel(dto);
            await wishListRepository.Update(wishList);
            WishListDto wishListDto = ModelToDto(wishList);

            return wishListDto;
        }

        public async Task<int> Delete(int id)
        {
            return await wishListRepository.Delete(id);
        }

        public async Task<WishListDto> Get(int id)
        {
            return ModelToDto(await wishListRepository.Get(id));
        }

        public List<WishListDto> GetAll()
        {
            List<WishList> wishLists = wishListRepository.GetAll();
            List<WishListDto> wishListDtos = ListModelToDto(wishLists);
            return wishListDtos;
        }

        private List<WishListDto> ListModelToDto(List<WishList> wishLists)
        {
            List<WishListDto> wishListDtos = new List<WishListDto>();
            foreach (WishList wishList in wishLists)
            {
                wishListDtos.Add(ModelToDto(wishList));
            }
            return wishListDtos;
        }

        private WishListDto ModelToDto(WishList wishList)
        {
            WishListDto wishListDto = new WishListDto
            {
                Id = wishList.Id,
                WishId = wishList.Id,
            };

            return wishListDto;
        }

        private WishList DtoToModel(WishListDto wishListDto)
        {
            WishList wishList = new WishList
            {
                Id = wishListDto.Id,
                WishId = wishListDto.WishId,
            };

            return wishList;
        }
    }
}
