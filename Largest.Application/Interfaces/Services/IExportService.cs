namespace Largest.Application.Interfaces.Services
{
    public interface IExportService
    {
        Task<byte[]> GenerateTransactionsPdfAsync(int userId, DateTime from, DateTime to);
    }
}
