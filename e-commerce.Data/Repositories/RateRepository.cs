using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly DataContext _context;

        public RateRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Rate> Add(Rate rate)
        {
            _context.Rates.Add(rate);

            await _context.SaveChangesAsync();

            return rate;
        }

        public async Task<Rate> Update(Rate rate)
        {
            _context.Rates.Update(rate);

            await _context.SaveChangesAsync();

            return rate;
        }

        public async Task<int> Delete(int id)
        {
            Rate rate = await _context.Rates.FindAsync(id);

            _context.Rates.Remove(rate);

            return await _context.SaveChangesAsync();
        }

        public async Task<Rate> Get(int id)
        {
            return await _context.Rates.FindAsync(id);
        }

        public List<Rate> GetAll()
        {
            return _context.Rates.ToList();
        }
    }
}