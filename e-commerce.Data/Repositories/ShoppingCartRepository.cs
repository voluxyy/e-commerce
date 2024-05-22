using ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> Add(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);

            await _context.SaveChangesAsync();

            return shoppingCart;
        }

        public async Task<ShoppingCart> Update(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);

            await _context.SaveChangesAsync();

            return shoppingCart;
        }

        public async Task<int> Delete(int id)
        {
            ShoppingCart shoppingCart = await _context.ShoppingCarts.FindAsync(id);

            _context.ShoppingCarts.Remove(shoppingCart);

            return await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCart> Get(int id)
        {
            return await _context.ShoppingCarts.FindAsync(id);
        }

        public async Task<ShoppingCart> GetFromUser(int id)
        {
            return await _context.ShoppingCarts.Where(x => x.UserId == id).FirstAsync();
        }

        public List<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts.ToList();
        }
    }
}