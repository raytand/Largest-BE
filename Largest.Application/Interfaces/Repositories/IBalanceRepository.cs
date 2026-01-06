using Largest.Domain.Entities;
using Largest.Domain.Enums;

namespace Largest.Application.Interfaces.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> AddAsync(Balance balance);
        Task<Balance?> GetByIdAsync(int balanceId);
        Task<List<Balance>> GetAllByUserAsync(int userId);
        Task UpdateAsync(Balance balance);
        Task DeleteAsync(Balance balance);
        Task<User?> GetUserByEmailAsync(string email);
        Task ShareBalanceAsync(Balance balance, int targetUserId, BalanceRole role);
        Task RemoveUserAsync(Balance balance, int targetUserId);
        bool IsUserOwner(Balance balance, int userId);
        bool CanUserEdit(Balance balance, int userId);
    }
}
