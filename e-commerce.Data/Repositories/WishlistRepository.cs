using ecommerce.Data.Repositories.Interface;
using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly DataContext _context;

        public WishlistRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Wishlist> Add(Wishlist wishlist)
        {
            _context.Wishlists.Add(wishlist);

            await _context.SaveChangesAsync();

            return wishlist;
        }

        public async Task<Wishlist> Update(Wishlist wishlist)
        {
            _context.Wishlists.Update(wishlist);

            await _context.SaveChangesAsync();

            return wishlist;
        }

        public async Task<int> Delete(int id)
        {
            Wishlist wishlist = await _context.Wishlists.FindAsync(id);

            _context.Wishlists.Remove(wishlist);

            return await _context.SaveChangesAsync();
        }

        public async Task<Wishlist> Get(int id)
        {
            return await _context.Wishlists.FindAsync(id);
        }

        public List<Wishlist> GetAll()
        {
            return _context.Wishlists.ToList();
        }
    }
}
