using Microsoft.EntityFrameworkCore;
using WOCS.Infrastructure.Data;
using WOCS.Infrastructure.Interfaces;

namespace WOCS.Infrastructure.Repositories
{
    public class ExproJobRepository : IExproJobRepository
    {
        private readonly WocsContext _context;

        public ExproJobRepository(WocsContext context)
        {
            _context = context;
        }
        public async Task<List<ExproJob>> GetTop10Async()
        {
            return await _context.ExproJobs
                                 .OrderByDescending(j => j.CreatedTime)
                                 .Take(10)
                                 .AsNoTracking()
                                 .ToListAsync()
                                 .ConfigureAwait(false);
        }
        public async Task<ExproJob?> GetByIdAsync(Guid id)
        {
            return await _context.ExproJobs
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(j => j.Id == id)
                                 .ConfigureAwait(false);
        }
    }
}
