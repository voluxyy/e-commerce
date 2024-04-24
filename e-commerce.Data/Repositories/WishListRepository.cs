using ecommerce.Data.Repositories.Interface;
using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly DataContext _context;

        public WishListRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<WishList> Add(WishList wishList)
        {
            _context.WishLists.Add(wishList);

            await _context.SaveChangesAsync();

            return wishList;
        }

        public async Task<WishList> Update(WishList wishList)
        {
            _context.WishLists.Update(wishList);

            await _context.SaveChangesAsync();

            return wishList;
        }

        public async Task<int> Delete(int id)
        {
            WishList wishList = await _context.WishLists.FindAsync(id);

            _context.WishLists.Remove(wishList);

            return await _context.SaveChangesAsync();
        }

        public async Task<WishList> Get(int id)
        {
            return await _context.WishLists.FindAsync(id);
        }

        public List<WishList> GetAll()
        {
            return _context.WishLists.ToList();
        }
    }
}
