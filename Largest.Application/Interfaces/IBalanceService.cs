
using Largest.Application.DTO_s;
using Largest.Domain.Entities;

namespace Largest.Application.Interfaces
{
    public interface IBalanceService
    {
        Task<Balance> CreateBalanceAsync(int userId, BalanceCreateDto dto);
        Task<Balance> UpdateBalanceAsync(int userId, int balanceId, decimal newAmount, string? newCurrency = null);
        Task DeleteBalanceAsync(int userId, int balanceId);
        Task<List<Balance>> GetAllBalancesAsync(int userId);
    }
}
