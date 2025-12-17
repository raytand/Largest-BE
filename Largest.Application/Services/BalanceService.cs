using Largest.Application.DTO_s;
using Largest.Application.DTOs;
using Largest.Application.Exceptions;
using Largest.Application.Interfaces;
using Largest.Domain.Entities;

namespace Largest.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _repo;
        public BalanceService(IBalanceRepository repo) => _repo = repo;

        public async Task<Balance> CreateBalanceAsync(int userId, BalanceCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new AppException("Balance name cannot be empty.");
            if (dto.InitialAmount < 0) throw new AppException("Initial amount cannot be negative.");

            var balance = new Balance
            {
                UserId = userId,
                Name = dto.Name.Trim(),
                Amount = dto.InitialAmount,
                Currency = dto.Currency
            };

            return await _repo.AddAsync(balance);
        }

        public async Task DeleteBalanceAsync(int userId, int balanceId)
        {
            var balance = await _repo.GetByIdAsync(balanceId);
            if (balance == null || balance.UserId != userId)
                throw new AppException("Balance not found or access denied.");
            await _repo.DeleteAsync(balance);
        }

        public async Task<List<Balance>> GetAllBalancesAsync(int userId)=>
             await _repo.GetAllByUserAsync(userId);
        

        public async Task<Balance> UpdateBalanceAsync(int userId, int balanceId, decimal newAmount, string? newCurrency = null)
        {
            var balance = await _repo.GetByIdAsync(balanceId);
            if (balance == null || balance.UserId != userId) throw new AppException("Balance not found or access denied.");
            if (newAmount < 0) throw new AppException("Balance amount cannot be negative.");

            balance.Amount = newAmount;
            if (!string.IsNullOrWhiteSpace(newCurrency)) balance.Currency = newCurrency.Trim();

            await _repo.UpdateAsync(balance);
            return balance;
        }
    }

}
