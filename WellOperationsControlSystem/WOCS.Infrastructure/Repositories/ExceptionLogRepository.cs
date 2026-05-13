using Microsoft.EntityFrameworkCore;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;

namespace WOCS.Infrastructure.Repositories
{
    public class ExceptionLogRepository : IExceptionLogRepository
    {

        private readonly WocsContext _context;

        public ExceptionLogRepository(WocsContext context)
        {
            _context = context;
        }


        public async Task AddAsync(ExceptionLogDto dto)
        {
            var entity = new ExceptionLog
            {
                Id = dto.Id,
                ErrorCode = dto.ErrorCode,
                Layer = dto.Layer,
                ExceptionType = dto.ExceptionType,
                Message = dto.Message,
                StackTrace = dto.StackTrace,
                InnerExceptionDetails = dto.InnerExceptionDetails,
                Context = dto.Context,
                ContextData = dto.ContextData,
                UserName = dto.UserName,
                MachineName = dto.MachineName,
                ApplicationVersion = dto.ApplicationVersion,
                ApplicationName = dto.ApplicationName,
                ThreadId = dto.ThreadId,
                ProcessId = dto.ProcessId,
                IsRecoverable = dto.IsRecoverable,
                ShouldTerminate = dto.ShouldTerminate,
                LogLevel = dto.LogLevel,
                OccurredAt = dto.OccurredAt,
                LoggedAt = dto.LoggedAt,
                IsReviewed = dto.IsReviewed,
                AdminNotes = dto.AdminNotes,
                RelatedEntityType = dto.RelatedEntityType,
                RelatedEntityId = dto.RelatedEntityId,
                CorrelationId = dto.CorrelationId,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };

            _context.ExceptionLogs.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExceptionLogDto>> GetAllAsync(int? count = null)
        {
            var query = _context.ExceptionLogs
                .AsNoTracking()
                .OrderByDescending(e => e.LoggedAt);

            if (count.HasValue)
            {
                query = (IOrderedQueryable<ExceptionLog>)query.Take(count.Value);
            }

            return await query.Select(e => new ExceptionLogDto
            {
                Id = e.Id,
                ErrorCode = e.ErrorCode,
                Layer = e.Layer,
                Message = e.Message,
                LogLevel = e.LogLevel,
                LoggedAt = e.LoggedAt,
                ApplicationName = e.ApplicationName,
                UserName = e.UserName
            }).ToListAsync();
        }

    }
}
