using Largest.Domain.Entities;

namespace Largest.Application.Interfaces.Repositories
{
    public interface IExportJobRepository
    {
        Task<ExportJob> CreateExportJobAsync(ExportJob exportJob);
        Task<ExportJob?> GetExportJobByIdAsync(Guid id, int userId);
        Task UpdateExportJobAsync(ExportJob exportJob);
    }
}
