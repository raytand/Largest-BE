using Largest.Application.Interfaces.Repositories;
using Largest.Domain.Entities;
using Largest.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Largest.Infrastructure.Data.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly AppDbContext _db;

        public BalanceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Balance> AddAsync(Balance balance)
        {
            await _db.Balances.AddAsync(balance);
            await _db.SaveChangesAsync();
            return balance;
        }

        public Task<Balance?> GetByIdAsync(int balanceId)
        {
            return _db.Balances
                .Include(b => b.BalanceUsers)
                .Include(b => b.Transactions)
                .FirstOrDefaultAsync(b => b.Id == balanceId && b.IsActive);
        }

        public Task<List<Balance>> GetAllByUserAsync(int userId)
        {
            return _db.Balances
                .Include(b => b.BalanceUsers).ThenInclude(bu => bu.User)
                .Include(b => b.Transactions)
                .Where(b => b.IsActive && b.BalanceUsers.Any(bu => bu.UserId == userId))
                .ToListAsync();
        }

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

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task ShareBalanceAsync(Balance balance, int targetUserId, BalanceRole role)
        {
            if (balance.BalanceUsers.Any(x => x.UserId == targetUserId))
                throw new InvalidOperationException("User already has access to this balance.");

            balance.BalanceUsers.Add(new BalanceUser
            {
                UserId = targetUserId,
                Role = role
            });

            await _db.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(Balance balance, int targetUserId)
        {
            var bu = balance.BalanceUsers.FirstOrDefault(x => x.UserId == targetUserId);
            if (bu != null)
            {
                balance.BalanceUsers.Remove(bu);
                await _db.SaveChangesAsync();
            }
        }

        public bool IsUserOwner(Balance balance, int userId)
        {
            return balance.BalanceUsers.Any(x => x.UserId == userId && x.Role == BalanceRole.Owner);
        }

        public bool CanUserEdit(Balance balance, int userId)
        {
            return balance.BalanceUsers.Any(x => x.UserId == userId &&
                (x.Role == BalanceRole.Owner || x.Role == BalanceRole.Editor));
        }
    }
}
