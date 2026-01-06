using Largest.Application.DTO_s;
using Largest.Application.Interfaces.Services;
using Largest.Domain.Enums;
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

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        private int GetUserId() => int.Parse(User.FindFirst("id")!.Value);

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int userId = GetUserId();
            var balances = await _balanceService.GetAllBalancesAsync(userId);
            var result = balances.Select(b =>
            {
                var userRole = b.BalanceUsers.First(x => x.UserId == userId).Role;
                return new BalanceDto
                {  
                    Id = b.Id,
                    Name = b.Name,
                    Amount = b.Amount,
                    Currency = b.Currency,
                    Role = userRole,
                    Users = b.BalanceUsers.Select(bu => new BalanceUserDto
                    {
                        Email = bu.User.Email,
                        Role = bu.Role
                    }).Where(bus => bus.Role != 0).ToList()
                };
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BalanceCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            int userId = GetUserId();
            var balance = await _balanceService.CreateBalanceAsync(userId, dto);
            var result = new BalanceDto
            {
                Id = balance.Id,
                Name = balance.Name,
                Amount = balance.Amount,
                Currency = balance.Currency,
                Role = BalanceRole.Owner
            };
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BalanceUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            int userId = GetUserId();
            var balance = await _balanceService.UpdateBalanceAsync(userId, dto);
            var result = new BalanceDto
            {
                Id = balance.Id,
                Name = balance.Name,
                Amount = balance.Amount,
                Currency = balance.Currency,
                Role = BalanceRole.Owner
            };
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = GetUserId();
            await _balanceService.DeleteBalanceAsync(userId, id);
            return NoContent();
        }

        [HttpPost("share/{balanceId}")]
        public async Task<IActionResult> Share(int balanceId, [FromBody] ShareBalanceDto dto)
        {
            int userId = GetUserId();
            await _balanceService.ShareBalance(balanceId, dto.Email, dto.Role, userId);
            return Ok();
        }

        [HttpDelete("{balanceId}/share/{targetUserId}")]
        public async Task<IActionResult> RemoveUser(int balanceId, int targetUserId)
        {
            int userId = GetUserId();
            await _balanceService.RemoveUser(balanceId, targetUserId, userId);
            return NoContent();
        }
    }
}
