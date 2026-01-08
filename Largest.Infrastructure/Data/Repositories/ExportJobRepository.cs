using Largest.Application.Interfaces.Repositories;
using Largest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Largest.Infrastructure.Data;

namespace Largest.Infrastructure.Data.Repositories
{
    public class ExportJobRepository : IExportJobRepository
    {
        private readonly AppDbContext _db;

        public ExportJobRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ExportJob> CreateExportJobAsync(ExportJob exportJob)
        {
            await _db.ExportJobs.AddAsync(exportJob);
            await _db.SaveChangesAsync();
            return exportJob;
        }

        public Task<ExportJob?> GetExportJobByIdAsync(Guid id, int userId)
        {
            return _db.ExportJobs.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        }

        public async Task UpdateExportJobAsync(ExportJob exportJob)
        {
            _db.ExportJobs.Update(exportJob);
            await _db.SaveChangesAsync();
        }
    }
}
