using Largest.Application.DTO_s;
using Largest.Application.Exceptions;
using Largest.Application.Interfaces.Repositories;
using Largest.Application.Interfaces.Services;
using Largest.Domain.Entities;
namespace Largest.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly IBalanceRepository _balanceRepo;
        private readonly ICategoryRepository _categoryRepo;

        public TransactionService(ITransactionRepository repo, IBalanceRepository balanceRepo, ICategoryRepository categoryRepo)
        {
            _repo = repo;
            _balanceRepo = balanceRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<Transaction> CreateTransactionAsync(int userId, TransactionCreateDto dto)
        {
            if (dto == null) throw new AppException("Transaction data is required.");
            if (dto.Amount <= 0) throw new AppException("Amount must be greater than zero.");
            if (dto.Date == default) throw new AppException("Invalid date.");

            var category = await _categoryRepo.GetByIdAsync(userId, dto.CategoryId);
            if (category == null || !category.IsActive)
                throw new AppException("Category is inactive or does not exist.");

            var balance = await _balanceRepo.GetByIdAsync(dto.BalanceId);
            if (balance == null || !balance.IsActive)
                throw new AppException("Balance is inactive or does not exist.");

            if (!dto.IsIncome && balance.Amount < dto.Amount)
                throw new AppException("Not enough balance for this transaction.");

            if (dto.IsIncome) balance.Amount += dto.Amount;
            else balance.Amount -= dto.Amount;

            await _balanceRepo.UpdateAsync(balance);

            var transaction = new Transaction
            {
                UserId = userId,
                Amount = dto.Amount,
                CategoryId = dto.CategoryId,
                BalanceId = dto.BalanceId,
                Description = dto.Description,
                Date = dto.Date,
                IsIncome = dto.IsIncome
            };

            return await _repo.AddTransactionAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int userId, int transactionId)
        {
            if (transactionId <= 0) throw new AppException("Invalid transaction ID.");

            var transaction = await _repo.GetByIdAsync(transactionId);
            if (transaction == null || transaction.UserId != userId)
                throw new AppException("Transaction not found or access denied.");


            var balance = await _balanceRepo.GetByIdAsync(transaction.BalanceId);
            if (balance != null)
            {
                if (transaction.IsIncome) balance.Amount -= transaction.Amount;
                else balance.Amount += transaction.Amount;
                await _balanceRepo.UpdateAsync(balance);
            }

            await _repo.DeleteTransactionAsync(transaction);
        }

        public async Task<List<TransactionDto>> GetAllTransactionsByDateIdAsync(int userId, DateTime from, DateTime to)
        {
            if (from == default || to == default || from > to)
                throw new AppException("Invalid date range.");

            var transactions = await _repo.GetByDateRangeAsync(userId, from, to);

            var dtoList = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                Description = t.Description,
                IsIncome = t.IsIncome,
                CategoryName = t.Category?.Name,
                BalanceName = t.Balance?.Name
            }).ToList();

            return dtoList;
        }

    }
}