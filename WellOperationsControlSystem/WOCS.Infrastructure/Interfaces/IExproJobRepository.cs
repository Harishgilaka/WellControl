using WOCS.Infrastructure.Data;

namespace WOCS.Infrastructure.Interfaces
{
    public interface IExproJobRepository
    {
        Task<List<ExproJob>> GetTop10Async();
        Task<ExproJob?> GetByIdAsync(Guid id);
    }
}
