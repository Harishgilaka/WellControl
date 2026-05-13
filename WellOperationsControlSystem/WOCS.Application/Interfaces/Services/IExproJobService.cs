using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Services
{
    public interface IExproJobService
    {
        Task<IEnumerable<ExproJobDto>> GetJobsAsync(int? count = null);
    }
}
