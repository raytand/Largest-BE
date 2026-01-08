using Largest.Application.DTO_s;
using Largest.Application.Interfaces.Repositories;
using Largest.Application.Interfaces.Services;
using Largest.Domain.Entities;


namespace Application.Services
{
    public class ExportService : IExportService
    {
        private readonly IExportJobRepository _repository;
        private readonly Application.Exports.PdfGenerator _pdfGenerator;
        private readonly ITransactionService _transactionService;

        public ExportService(IExportJobRepository repository, Application.Exports.PdfGenerator pdfGenerator, ITransactionService transactionService)
        {
            _repository = repository;
            _pdfGenerator = pdfGenerator;
            _transactionService = transactionService;
        }

        public async Task<byte[]> GenerateTransactionsPdfAsync(int userId, DateTime from, DateTime to)
        {
            var job = new ExportJob
            {
                UserId = userId,
                From = from,
                To = to
            };
            await _repository.CreateExportJobAsync(job);
            var transactions = await _transactionService.GetAllTransactionsByDateIdAsync(userId, from, to);

            var pdfBytes = _pdfGenerator.GenerateTransactionsPdf(transactions);

            job.FileName = $"export_{job.Id}.pdf";
            await _repository.UpdateExportJobAsync(job);

            return pdfBytes;
        }
    }
}
