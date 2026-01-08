using Largest.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Largest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExportsController : ControllerBase
    {
        private readonly IExportService _exportService;
        public ExportsController(IExportService exportService)
        {
            _exportService = exportService;
        }
        [HttpGet("ExportTransactions")]
        public async Task<IActionResult> ExportTransactions(DateTime from, DateTime to)
        {
            int userId = int.Parse(User.FindFirst("id")!.Value);
            var pdfBytes = await _exportService.GenerateTransactionsPdfAsync(userId, from, to);
            return File(pdfBytes, "application/pdf", $"export_{userId}_{from.ToShortDateString()}_{to.ToShortDateString()}.pdf");
        }
    }
}
