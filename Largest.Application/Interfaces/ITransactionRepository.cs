using Largest.Domain.Entities;

namespace Largest.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction?> GetByIdAsync(int transactionId);
        Task<List<Transaction>> GetByDateRangeAsync(int userId, DateTime from, DateTime to);
        Task DeleteTransactionAsync(Transaction transaction);
    }
}
