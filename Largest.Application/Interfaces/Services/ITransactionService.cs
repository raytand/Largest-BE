using Largest.Domain.Entities;
using Largest.Application.DTO_s;

namespace Largest.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(int userId, TransactionCreateDto dto);
        Task DeleteTransactionAsync(int userId, int transactionId);
        Task<List<TransactionDto>> GetAllTransactionsByDateIdAsync(int userId, DateTime from, DateTime to);
    }
}
