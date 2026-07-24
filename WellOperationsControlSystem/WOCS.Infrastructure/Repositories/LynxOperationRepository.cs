using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;
using Enum = WOCS.Domain.Enums;

namespace WOCS.Infrastructure.Repositories
{
    public class LynxOperationRepository : ILynxOperationRepository
    {
        private readonly WocsContext _context;
        public LynxOperationRepository(WocsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OperationDto>> FindAsync(Expression<Func<OperationDto, bool>> predicate)
        {
            var query = _context.LynxOperations.AsQueryable();

            return await query.Select(x => new OperationDto
            {
                Id = x.Id,
                JobConfigurationId = x.JobConfigurationId,
                Name = x.Name,
                WellId = x.WellId,
                IsActive = x.IsActive,
                OperationTypeId = x.OperationTypeId,
                OperationType = (Enum.LynxOperationType)x.OperationTypeId,
                ExproWell = new ExproWellDto
                {
                    Id = x.Well.Id,
                    Name = x.Well.Name
                }
            }).Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<OperationDto>> GetAllAsync(int? count = null)
        {
            var query = _context.LynxOperations.AsQueryable();

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.Select(x => new OperationDto
            {
                Id = x.Id,
                JobConfigurationId = x.JobConfigurationId,
                Name = x.Name,
                WellId = x.WellId,
                IsActive = x.IsActive,
                OperationTypeId = x.OperationTypeId,
                OperationType = (Enum.LynxOperationType)x.OperationTypeId,
                ExproWell = new ExproWellDto
                {
                    Id = x.Well.Id,
                    Name = x.Well.Name
                }
            }).ToListAsync();
        }

        public async Task<IEnumerable<OperationDto>> GetOperationsWithJobIdAsync(Guid jobId)
        {
            var query = _context.LynxOperations.Where(x => x.JobConfigurationId == jobId && x.OperationTypeId == (int)Enum.LynxOperationType.EM).AsQueryable();

            if (query.Any())
            {
                return await query.Select(x => new OperationDto
                {
                    Id = x.Id,
                    JobConfigurationId = x.JobConfigurationId,
                    Name = x.Name,
                    WellId = x.WellId,
                    IsActive = x.IsActive,
                    OperationTypeId = x.OperationTypeId,
                    OperationType = (Enum.LynxOperationType)x.OperationTypeId,
                    ExproWell = new ExproWellDto
                    {
                        Id = x.Well.Id,
                        Name = x.Well.Name,
                        Location = x.Well.Location,
                        WellTypeId = x.Well.WellTypeId,
                        WellType = (Enum.ExproWellType)x.Well.WellTypeId,
                        FluidTypeId = x.Well.FluidTypeId,
                        FluidType = (Enum.ExproFluidType)x.Well.FluidTypeId,
                        TimeZoneId = x.Well.TimeZoneId
                    }
                }).ToListAsync();
            }

            return Enumerable.Empty<OperationDto>();
        }
    }
}
