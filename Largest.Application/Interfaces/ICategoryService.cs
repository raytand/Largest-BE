using Largest.Domain.Entities;

namespace Largest.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(int userId, string categoryName);
        Task DeleteCategoryAsync(int userId, int categoryId);
        Task<List<Category>> GetAllCategoriesByUserIdAsync(int userId);
    }
}
