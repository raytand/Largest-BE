using Largest.Application.Interfaces;
using Largest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Largest.Infrastructure.Data.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly AppDbContext _db;
        public BalanceRepository(AppDbContext db) => _db = db;

        public async Task<Balance> AddAsync(Balance balance)
        {
            await _db.Balances.AddAsync(balance);
            await _db.SaveChangesAsync();
            return balance;
        }

        public Task<Balance?> GetByIdAsync(int? balanceId) =>       
            _db.Balances.Include(b => b.Transactions).FirstOrDefaultAsync(b => b.Id == balanceId);

        public Task<List<Balance>> GetAllByUserAsync(int userId) =>
            _db.Balances.Include(b => b.Transactions).Where(b => b.UserId == userId && b.IsActive == true).ToListAsync();

        public async Task UpdateAsync(Balance balance)
        {
            _db.Balances.Update(balance);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Balance balance)
        {
            balance.IsActive = false;
            _db.Balances.Update(balance);
            await _db.SaveChangesAsync();
        }
    }

}
