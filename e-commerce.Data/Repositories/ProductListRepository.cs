using ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data.Repositories
{
    public class ProductListRepository : IProductListRepository
    {
        private readonly DataContext _context;

        public ProductListRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ProductList> Add(ProductList productList)
        {
            _context.ProductLists.Add(productList);

            await _context.SaveChangesAsync();

            return productList;
        }

        public async Task<ProductList> Update(ProductList productList)
        {
            _context.ProductLists.Update(productList);

            await _context.SaveChangesAsync();

            return productList;
        }

        public async Task<int> Delete(int id)
        {
            ProductList productList = await _context.ProductLists.FindAsync(id);

            _context.ProductLists.Remove(productList);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteFromProduct(int id)
        {
            ProductList productList = await _context.ProductLists.Where(x => x.ProductId == id).FirstAsync();

            _context.ProductLists.Remove(productList);

            return await _context.SaveChangesAsync();
        }

        public async Task<ProductList> Get(int id)
        {
            return await _context.ProductLists.FindAsync(id);
        }

        public async Task<List<ProductList>> GetFromShoppingCart(int id)
        {
            return await _context.ProductLists.Where(x => x.ShoppingCartId == id).ToListAsync();
        }

        public List<ProductList> GetAll()
        {
            return _context.ProductLists.ToList();
        }
    }
}