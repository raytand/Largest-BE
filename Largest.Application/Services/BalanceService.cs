using Largest.Application.DTO_s;
using Largest.Application.Exceptions;
using Largest.Application.Interfaces.Repositories;
using Largest.Application.Interfaces.Services;
using Largest.Domain.Entities;
using Largest.Domain.Enums;

namespace Largest.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _repo;

        public BalanceService(IBalanceRepository repo)
        {
            _repo = repo;
        }

        public async Task<Balance> CreateBalanceAsync(int userId, BalanceCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new AppException("Balance name cannot be empty.");
            if (dto.InitialAmount < 0)
                throw new AppException("Initial amount cannot be negative.");

            var balance = new Balance
            {
                Name = dto.Name.Trim(),
                Amount = dto.InitialAmount,
                Currency = dto.Currency,
                BalanceUsers = new List<BalanceUser>
                {
                    new BalanceUser
                    {
                        UserId = userId,
                        Role = BalanceRole.Owner
                    }
                }
            };

            return await _repo.AddAsync(balance);
        }

        public async Task<List<Balance>> GetAllBalancesAsync(int userId)
        {
            return await _repo.GetAllByUserAsync(userId);
        }

        public async Task<Balance> UpdateBalanceAsync(int userId, BalanceUpdateDto dto)
        {
            var balance = await _repo.GetByIdAsync(dto.BalanceId);
            if (balance == null)
                throw new AppException("Balance not found.");

            if (!_repo.CanUserEdit(balance, userId))
                throw new AppException("Access denied.");

            if (dto.Amount < 0)
                throw new AppException("Balance amount cannot be negative.");

            balance.Amount = dto.Amount;
            if (!string.IsNullOrWhiteSpace(dto.Currency) || !string.IsNullOrWhiteSpace(dto.Name))
            {
                balance.Name = dto.Name;
                balance.Currency = dto.Currency.Trim();
            }

            await _repo.UpdateAsync(balance);
            return balance;
        }

        public async Task DeleteBalanceAsync(int userId, int balanceId)
        {
            var balance = await _repo.GetByIdAsync(balanceId);
            if (balance == null)
                throw new AppException("Balance not found.");

            if (!_repo.IsUserOwner(balance, userId))
                throw new AppException("Only owner can delete balance.");

            await _repo.DeleteAsync(balance);
        }

        public async Task ShareBalance(int balanceId, string email, BalanceRole role, int currentUserId)
        {
            var balance = await _repo.GetByIdAsync(balanceId);
            if (balance == null)
                throw new AppException("Balance not found.");

            if (!_repo.IsUserOwner(balance, currentUserId))
                throw new AppException("Only owner can share balance.");

            var user = await _repo.GetUserByEmailAsync(email);
            if (user == null)
                throw new AppException("User not found.");

            await _repo.ShareBalanceAsync(balance, user.Id, role);
        }

        public async Task RemoveUser(int balanceId, int targetUserId, int currentUserId)
        {
            var balance = await _repo.GetByIdAsync(balanceId);
            if (balance == null)
                throw new AppException("Balance not found.");

            if (!_repo.IsUserOwner(balance, currentUserId))
                throw new AppException("Only owner can remove users.");

            await _repo.RemoveUserAsync(balance, targetUserId);
        }
    }
}
