using Largest.Application.Interfaces.Repositories;
using Largest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Largest.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) { _db = db; }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }
        public async Task DeleteCategoryAsync(int userId, int categoryId)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
            if (category != null)
            {
                category.IsActive = false;
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<Category?> GetByNameAsync(int userId, string categoryName)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Name == categoryName && c.UserId == userId);
        }
        public async Task<Category?> GetByIdAsync(int userId, int categoryId)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
        }
        public async Task<List<Category>> GetAllByUserIdAsync(int userId)
        {
            return await _db.Categories.Where(c => c.UserId == userId && c.IsActive == true).ToListAsync();
        }
    }
}
