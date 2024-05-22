using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class ProductListService : IProductListService
    {
        private readonly IProductListRepository productListRepository;

        public ProductListService(IProductListRepository productListRepository)
        {
            this.productListRepository = productListRepository;
        }

        public async Task<ProductListDto> Add(ProductListDto dto)
        {
            ProductList productList = DtoToModel(dto);
            await productListRepository.Add(productList);
            ProductListDto productListDto = ModelToDto(productList);

            return productListDto;
        }

        public async Task<ProductListDto> Update(ProductListDto dto)
        {
            ProductList productList = DtoToModel(dto);
            await productListRepository.Update(productList);
            ProductListDto productListDto = ModelToDto(productList);

            return productListDto;
        }

        public async Task<int> Delete(int id)
        {
            return await productListRepository.Delete(id);
        }

        public async Task<int> DeleteFromProduct(int id)
        {
            return await productListRepository.DeleteFromProduct(id);
        }

        public async Task<ProductListDto> Get(int id)
        {
            return ModelToDto(await productListRepository.Get(id));
        }

        public async Task<List<ProductListDto>> GetFromShoppingCart(int id)
        {
            return ListModelToDto(await productListRepository.GetFromShoppingCart(id));
        }

        public List<ProductListDto> GetAll()
        {
            List<ProductList> productLists = productListRepository.GetAll();
            List<ProductListDto> productListsDtos = ListModelToDto(productLists);
            return productListsDtos;
        }

        private List<ProductListDto> ListModelToDto(List<ProductList> productLists)
        {
            List<ProductListDto> productListsDtos = new List<ProductListDto>();
            foreach (ProductList productList in productLists)
            {
                productListsDtos.Add(ModelToDto(productList));
            }
            return productListsDtos;
        }

        private ProductListDto ModelToDto(ProductList productList)
        {
            ProductListDto productListDto = new ProductListDto
            {
                Id = productList.Id,
                ProductId = productList.ProductId,
                ShoppingCartId = productList.ShoppingCartId
            };

            return productListDto;
        }

        private ProductList DtoToModel(ProductListDto productListDto)
        {
            ProductList productList = new ProductList
            {
                Id = productListDto.Id,
                ProductId = productListDto.ProductId,
                ShoppingCartId = productListDto.ShoppingCartId
            };

            return productList;
        }
    }
}