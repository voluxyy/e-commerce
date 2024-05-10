using ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<int> Delete(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetLast()
        {
            return await _context.Products.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
    }
}