using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;

namespace WOCS.Application.Services
{
    public class ExproJobService : IExproJobService
    {
        private readonly IExproJobRepository _repository;
        private readonly IExceptionLogService _exceptionLogService;

        public ExproJobService(IExproJobRepository repository, IExceptionLogService exceptionLogService)
        {
            _repository = repository;
            _exceptionLogService = exceptionLogService;
        }

        public async Task<IEnumerable<ExproJobDto>> GetJobsAsync(int? count = null)
        {
            return await _repository.GetAllAsync(count);
        }
    }
}
