using ecommerce.Data.Repositories.Interface;
using ecommerce.Data.Models;

namespace ecommerce.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category category)
        {
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Update(Category category)
        {
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<int> Delete(int id)
        {
            Category category = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(category);

            return await _context.SaveChangesAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
    }
}
