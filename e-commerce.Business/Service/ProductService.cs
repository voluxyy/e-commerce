using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductDto> Add(ProductDto dto)
        {
            Product product = DtoToModel(dto);
            await productRepository.Add(product);
            ProductDto productDto = ModelToDto(product);

            return productDto;
        }

        public async Task<ProductDto> Update(ProductDto dto)
        {
            Product product = DtoToModel(dto);
            await productRepository.Update(product);
            ProductDto productDto = ModelToDto(product);

            return productDto;
        }

        public async Task<int> Delete(int id)
        {
            return await productRepository.Delete(id);
        }

        public async Task<ProductDto> Get(int id)
        {
            return ModelToDto(await productRepository.Get(id));
        }

        public List<ProductDto> GetAll()
        {
            List<Product> products = productRepository.GetAll();
            List<ProductDto> productsDtos = ListModelToDto(products);
            return productsDtos;
        }

        private List<ProductDto> ListModelToDto(List<Product> products)
        {
            List<ProductDto> productsDtos = new List<ProductDto>();
            foreach (Product product in products)
            {
                productsDtos.Add(ModelToDto(product));
            }
            return productsDtos;
        }

        private ProductDto ModelToDto(Product product)
        {
            ProductDto productDto = new ProductDto
            {
                Id = product.Id,
                ImagePath = product.ImagePath,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
            };

            return productDto;
        }

        private Product DtoToModel(ProductDto productDto)
        {
            Product product = new Product
            {
                Id = productDto.Id,
                ImagePath = productDto.ImagePath,
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,
            };

            return product;
        }
    }
}