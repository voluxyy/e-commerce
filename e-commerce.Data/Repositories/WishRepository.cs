using ecommerce.Data.Repositories.Interface;
using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public class WishRepository : IWishRepository
    {
        private readonly DataContext _context;

        public WishRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Wish> Add(Wish wish)
        {
            _context.Wishs.Add(wish);

            await _context.SaveChangesAsync();

            return wish;
        }

        public async Task<Wish> Update(Wish wish)
        {
            _context.Wishs.Update(wish);

            await _context.SaveChangesAsync();

            return wish;
        }

        public async Task<int> Delete(int id)
        {
            Wish wish = await _context.Wishs.FindAsync(id);

            _context.Wishs.Remove(wish);

            return await _context.SaveChangesAsync();
        }

        public async Task<Wish> Get(int id)
        {
            return await _context.Wishs.FindAsync(id);
        }

        public List<Wish> GetAll()
        {
            return _context.Wishs.ToList();
        }
    }
}
