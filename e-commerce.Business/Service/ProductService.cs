using System.IO.Enumeration;
using System.Security;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ecommerce.Business.Service
{
    public class ProductService : IProductService
    {
        private string varFolderPath;
        private string uploadsFolderPath;

        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            string pathToFront = "../front/src/";

            this.varFolderPath = Path.Combine(Directory.GetCurrentDirectory(), pathToFront);
            this.uploadsFolderPath = Path.Combine(this.varFolderPath, "assets");
            this.productRepository = productRepository;
        }

        public async Task<ProductDto> Add(ProductDto dto, byte[] imageData)
        {
            Product lastProduct = await productRepository.GetLast();

            int Id = (lastProduct != null) ? lastProduct.Id : 0;

            dto.ImagePath = await SaveImage(Id+1, dto.Name, imageData);

            Product product = DtoToModel(dto);
            await productRepository.Add(product);
            ProductDto productDto = ModelToDto(product);

            return productDto;
        }

        public async Task<ProductDto> Update(ProductDto dto, byte[] imageData)
        {
            if (dto.ImagePath == null) {
                throw new ArgumentNullException("ImagePath is missing.");
            }

            UpdateImage(dto.ImagePath, imageData);

            Product product = DtoToModel(dto);
            await productRepository.Update(product);
            ProductDto productDto = ModelToDto(product);

            return productDto;
        }

        public async Task<int> Delete(int id)
        {
            Product product = await productRepository.Get(id);

            if (product.ImagePath != null) {
                File.Delete(Path.Combine(this.uploadsFolderPath, product.ImagePath));
            }

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

        private async Task<string> SaveImage(int id, string fileName, byte[] imageData)
        {
            /*this.CheckFolders();*/

            string uniqueFileName = "picture_" + id + "-" + fileName + ".jpg";
            string filePath = Path.Combine(this.uploadsFolderPath, uniqueFileName);

            _ = File.WriteAllBytesAsync(filePath, imageData);

            return uniqueFileName;
        }

        private async void UpdateImage(string path, byte[] imageData)
        {
/*            this.CheckFolders();
*/
            string filePath = Path.Combine(this.uploadsFolderPath, path);

            _ = File.WriteAllBytesAsync(filePath, imageData);
        }

        /*private void CheckFolders() {
            if (!Directory.Exists(this.varFolderPath)) {
                Directory.CreateDirectory(this.varFolderPath);
            }

            if (!Directory.Exists(this.uploadsFolderPath)) {
                Directory.CreateDirectory(this.uploadsFolderPath);
            }
        }*/
    }
}