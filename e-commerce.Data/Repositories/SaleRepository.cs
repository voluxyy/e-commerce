using ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _context;

        public SaleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Sale> Add(Sale sale)
        {
            _context.Sales.Add(sale);

            await _context.SaveChangesAsync();

            return sale;
        }

        public async Task<Sale> Update(Sale sale)
        {
            _context.Sales.Update(sale);

            await _context.SaveChangesAsync();

            return sale;
        }

        public async Task<int> Delete(int id)
        {
            Sale sale = await _context.Sales.FindAsync(id);

            _context.Sales.Remove(sale);

            return await _context.SaveChangesAsync();
        }

        public async Task<Sale> Get(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public List<Sale> GetAll()
        {
            return _context.Sales.ToList();
        }

        public async Task<List<Sale>> GetFromUser(int id)
        {
            return await _context.Sales.Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<List<Sale>> GetLast7Days()
        {
            var sevenDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-7));
            return await _context.Sales.Where(x => x.Date >= sevenDaysAgo).ToListAsync();
        }
    }
}