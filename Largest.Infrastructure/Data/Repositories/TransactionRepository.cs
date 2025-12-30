using Largest.Application.Interfaces.Repositories;
using Largest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Largest.Infrastructure.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();
            return transaction;
        }

        public Task<Transaction?> GetByIdAsync(int transactionId)
        {
            return _db.Transactions.Include(t => t.Balance).Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public Task<List<Transaction>> GetByDateRangeAsync(int userId, DateTime from, DateTime to)
        {
            return _db.Transactions
               .Include(t => t.Balance)
               .Include(t => t.Category)
               .Where(t => t.UserId == userId && t.Date >= from && t.Date <= to)
               .ToListAsync();
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();
        }
    }
}
