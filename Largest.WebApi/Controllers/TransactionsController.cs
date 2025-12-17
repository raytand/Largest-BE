using Largest.Application.DTO_s;
using Largest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Largest.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService) => _transactionService = transactionService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirst("id")!.Value);
            var transaction = await _transactionService.CreateTransactionAsync(userId, dto);

            var result = new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date,
                IsIncome = transaction.IsIncome,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category?.Name,
                BalanceName = transaction.Balance?.Name
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var list = await _transactionService.GetAllTransactionsByDateIdAsync(userId, from, to);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            await _transactionService.DeleteTransactionAsync(userId, id);
            return NoContent();
        }
    }

}
