using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCartDto> Add(ShoppingCartDto dto)
        {
            ShoppingCart shoppingCart = DtoToModel(dto);
            await shoppingCartRepository.Add(shoppingCart);
            ShoppingCartDto shoppingCartDto = ModelToDto(shoppingCart);

            return shoppingCartDto;
        }

        public async Task<ShoppingCartDto> Update(ShoppingCartDto dto)
        {
            ShoppingCart shoppingCart = DtoToModel(dto);
            await shoppingCartRepository.Update(shoppingCart);
            ShoppingCartDto shoppingCartDto = ModelToDto(shoppingCart);

            return shoppingCartDto;
        }

        public async Task<int> Delete(int id)
        {
            return await shoppingCartRepository.Delete(id);
        }

        public async Task<ShoppingCartDto> Get(int id)
        {
            return ModelToDto(await shoppingCartRepository.Get(id));
        }

        public async Task<ShoppingCartDto> GetFromUser(int id)
        {
            return ModelToDto(await shoppingCartRepository.GetFromUser(id));
        }

        public List<ShoppingCartDto> GetAll()
        {
            List<ShoppingCart> shoppingCarts = shoppingCartRepository.GetAll();
            List<ShoppingCartDto> shoppingCartsDtos = ListModelToDto(shoppingCarts);
            return shoppingCartsDtos;
        }

        private List<ShoppingCartDto> ListModelToDto(List<ShoppingCart> shoppingCarts)
        {
            List<ShoppingCartDto> shoppingCartsDtos = new List<ShoppingCartDto>();
            foreach (ShoppingCart shoppingCart in shoppingCarts)
            {
                shoppingCartsDtos.Add(ModelToDto(shoppingCart));
            }
            return shoppingCartsDtos;
        }

        private ShoppingCartDto ModelToDto(ShoppingCart shoppingCart)
        {
            ShoppingCartDto shoppingCartDto = new ShoppingCartDto
            {
                Id = shoppingCart.Id,
                UserId = shoppingCart.UserId
            };

            return shoppingCartDto;
        }

        private ShoppingCart DtoToModel(ShoppingCartDto shoppingCartDto)
        {
            ShoppingCart shoppingCart = new ShoppingCart
            {
                Id = shoppingCartDto.Id,
                UserId = shoppingCartDto.UserId
            };

            return shoppingCart;
        }
    }
}