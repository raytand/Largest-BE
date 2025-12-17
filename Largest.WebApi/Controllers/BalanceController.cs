using Largest.Application.DTO_s;
using Largest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Largest.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/balances")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        public BalanceController(IBalanceService balanceService) { _balanceService = balanceService; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var balances = await _balanceService.GetAllBalancesAsync(userId);
            var result = balances.Select(b => new BalanceDto
            {
                Id = b.Id,
                Name = b.Name,
                Amount = b.Amount,
                Currency = b.Currency
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BalanceCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var balance = await _balanceService.CreateBalanceAsync(userId, dto);
            return Ok(balance);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BalanceUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var balance = await _balanceService.UpdateBalanceAsync(userId, dto.BalanceId, dto.Amount, dto.Currency);
            return Ok(balance);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            await _balanceService.DeleteBalanceAsync(userId, id);
            return NoContent();
        }
    }

}
