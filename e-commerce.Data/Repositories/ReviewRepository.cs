using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Review> Add(Review review)
        {
            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<Review> Update(Review review)
        {
            _context.Reviews.Update(review);

            await _context.SaveChangesAsync();

            return review;
        }

        public async Task<int> Delete(int id)
        {
            Review review = await _context.Reviews.FindAsync(id);

            _context.Reviews.Remove(review);

            return await _context.SaveChangesAsync();
        }

        public async Task<Review> Get(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public List<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public List<Review> GetFromProduct(int id)
        {
            return _context.Reviews.Where(x => x.ProductId == id).ToList();
        }
    }
}