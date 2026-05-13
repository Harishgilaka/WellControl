using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Repositories
{
    public interface IExceptionLogRepository
    {
        Task AddAsync(ExceptionLogDto log);
        Task<IEnumerable<ExceptionLogDto>> GetAllAsync(int? count = null);
    }
}
