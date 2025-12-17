using Largest.Domain.Entities;

namespace Largest.Application.Interfaces
{
    public interface IBalanceRepository
    {
        Task<Balance> AddAsync(Balance balance);
        Task<Balance?> GetByIdAsync(int? balanceId);
        Task<List<Balance>> GetAllByUserAsync(int userId);
        Task UpdateAsync(Balance balance);
        Task DeleteAsync(Balance balance);
    }
}