using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;

namespace WOCS.Infrastructure.Repositories
{
    public class ExproJobRepository : IExproJobRepository
    {
        private readonly WocsContext _context;

        public ExproJobRepository(WocsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExproJobDto>> FindAsync(Expression<Func<ExproJobDto, bool>> predicate)
        {
            var query = _context.ExproJobs.AsQueryable();

            var result = await query
                    .Select(x => new ExproJobDto
                    {
                        Id = x.Id,
                        ClientId = x.ClientId,
                        Name = x.Name,
                        ClientName = x.Client.Name,
                        ContactAddress = x.ContactAddress,
                        IsActive = x.IsActive
                    })
                    .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ExproJobDto>> GetAllAsync(int? count = null)
        {
            var query = _context.ExproJobs.AsQueryable();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query
                    .Select(x => new ExproJobDto
                    {
                        Id = x.Id,
                        ClientId = x.ClientId,
                        Name = x.Name,
                        ClientName = x.Client.Name,
                        ContactAddress = x.ContactAddress,
                        IsActive = x.IsActive
                    })
                    .ToListAsync();
        }
    }
}
