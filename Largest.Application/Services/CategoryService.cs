using Largest.Application.Exceptions;
using Largest.Application.Interfaces.Repositories;
using Largest.Application.Interfaces.Services;
using Largest.Domain.Entities;

namespace Largest.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<Category> CreateCategoryAsync(int userId, string categoryName, bool isIncome)
        {
            if(string.IsNullOrWhiteSpace(categoryName))
            {
                throw new AppException("Category name cannot be empty.");
            }
            var existingCategory = await _repo.GetByNameAsync(userId, categoryName);
            if (existingCategory != null)
            {
                throw new AppException("Category with this name already exists.");
            }

            var category = new Category { Name = categoryName.Trim(), UserId = userId, IsIncome = isIncome };
            return await _repo.AddCategoryAsync(category);
        }
        public async Task DeleteCategoryAsync(int userId, int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new AppException("Invalid category ID.");
            }
            var category = await _repo.GetByIdAsync(userId, categoryId);
            if (category == null)
            {
                throw new AppException("Category not found or access denied.");
            }
            await _repo.DeleteCategoryAsync(userId, categoryId);
        }
        public async Task<List<Category>> GetAllCategoriesByUserIdAsync(int userId)
        {
            return await _repo.GetAllByUserIdAsync(userId);
        }
    }
}
