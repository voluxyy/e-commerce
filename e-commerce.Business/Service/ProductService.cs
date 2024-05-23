using System.IO.Enumeration;
using System.Security;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;
using ecommerce.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ecommerce.Business.Service
{
    public class ProductService : IProductService
    {
        private string varFolderPath;
        private string uploadsFolderPath;

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            string pathToFront = "../front/src/";

            this.varFolderPath = Path.Combine(Directory.GetCurrentDirectory(), pathToFront);
            this.uploadsFolderPath = Path.Combine(this.varFolderPath, "assets");
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<ProductDto> Add(ProductDto dto, byte[] imageData)
        {
            Product lastProduct = await productRepository.GetLast();

            int Id = (lastProduct != null) ? lastProduct.Id : 0;

            dto.ImagePath = await SaveImage(Id + 1, dto.Name, imageData);

            Product product = DtoToModel(dto);
            await productRepository.Add(product);
            ProductDto productDto = ModelToDto(product);

            return productDto;
        }

        public async Task<ProductDto> Update(ProductDto dto, byte[] imageData)
        {
            Product product = await productRepository.Get(dto.Id);

            product.Name = (dto.Name != null) ? dto.Name : product.Name;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.CategoryId = dto.CategoryId;

            if (imageData != null)
                UpdateImage(product.ImagePath, imageData);

            await productRepository.Update(product);
            ProductDto productDto = ModelToDto(product);

            return productDto;
        }

        public async Task<int> Delete(int id)
        {
            Product product = await productRepository.Get(id);

            if (product.ImagePath != null)
            {
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
                ImagePath = productDto.ImagePath!,
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,
            };

            return product;
        }

        private async Task<string> SaveImage(int id, string fileName, byte[] imageData)
        {
            this.CheckFolders();

            string uniqueFileName = "picture_" + id + "-" + fileName + ".jpg";
            string filePath = Path.Combine(this.uploadsFolderPath, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, imageData);

            return uniqueFileName;
        }

        private async void UpdateImage(string path, byte[] imageData)
        {
            this.CheckFolders();
            string filePath = Path.Combine(this.uploadsFolderPath, path);

            await File.WriteAllBytesAsync(filePath, imageData);
        }

        private void CheckFolders() {
            if (!Directory.Exists(this.varFolderPath)) {
                Directory.CreateDirectory(this.varFolderPath);
            }

            if (!Directory.Exists(this.uploadsFolderPath)) {
                Directory.CreateDirectory(this.uploadsFolderPath);
            }
        }

        public List<ProductDto> SearchBar(string searchItems)
        {
            List<Product> allProducts = productRepository.GetAll();
            List<ProductDto> searchedProducts = new List<ProductDto>();

            string searchTermLower = searchItems.ToLower();

            foreach (Product product in allProducts)
            {
                string productName = product.Name.ToLower();

                Console.WriteLine(searchTermLower);
                Console.WriteLine(productName);

                if (productName.Contains(searchTermLower) || Levenshtein(productName, searchTermLower))
                {
                    searchedProducts.Add(ModelToDto(product));
                }
                
            }
            return searchedProducts;
        }
        private bool Levenshtein(string input, string searchTerm)
        {
            int n = input.Length;
            int m = searchTerm.Length;

            int[,] distance = new int[n + 1, m + 1];

            if (n == 0) return m <= 2;
            if (m == 0) return n <= 2;

            for (int i = 0; i < n; i++)
            {
                distance[i, 0] = i;
            }
            for (int j = 0; j <= m; j++)
            {
                distance[0, j] = j;
            }

            for (int j = 1; j <= m; j++)
            {
                for (int i = 1; i <= n; i++)
                {
                    int cost = (input[i - 1] == searchTerm[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(Math.Min(
                        distance[i - 1, j] + 1,
                        distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[n, m] <= 2;
        }
    }
}