using ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Admin> Add(Admin admin)
        {
            _context.Admins.Add(admin);

            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin> Update(Admin admin)
        {
            _context.Admins.Update(admin);

            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<int> Delete(Guid id)
        {
            Admin admin = await _context.Admins.FindAsync(id);

            _context.Admins.Remove(admin);

            return await _context.SaveChangesAsync();
        }

        public async Task<Admin> Get(Guid id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<Admin> GetByEmail(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.Email == email);
        }

        public List<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }
    }
}