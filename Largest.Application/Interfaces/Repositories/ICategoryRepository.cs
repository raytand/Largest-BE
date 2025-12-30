using Largest.Domain.Entities;

namespace Largest.Application.Interfaces.Repositories
{

    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int userId, int categoryId);
        Task<Category?> GetByIdAsync(int userId, int categoryId);
        Task<Category?> GetByNameAsync(int userId, string categoryName);
        Task<List<Category>> GetAllByUserIdAsync(int userId);
    }
}
