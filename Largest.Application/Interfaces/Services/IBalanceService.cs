using Largest.Application.DTO_s;
using Largest.Domain.Entities;
using Largest.Domain.Enums;

namespace Largest.Application.Interfaces.Services
{
    public interface IBalanceService
    {
        Task<Balance> CreateBalanceAsync(int userId, BalanceCreateDto dto);
        Task<Balance> UpdateBalanceAsync(int userId, BalanceUpdateDto dto);
        Task DeleteBalanceAsync(int userId, int balanceId);
        Task<List<Balance>> GetAllBalancesAsync(int userId);
        Task ShareBalance(int balanceId, string email, BalanceRole role, int currentUserId);
        Task RemoveUser(int balanceId, int targetUserId, int currentUserId);
    }
}
