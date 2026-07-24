using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;

namespace WOCS.Application.Services
{
    public class LynxOperationService : ILynxOperationService
    {
        private readonly IExceptionLogService _exceptionLogService;
        private readonly ILynxOperationRepository _repository;

        public LynxOperationService(IExceptionLogService exceptionLogService, ILynxOperationRepository repository)
        {
            _exceptionLogService = exceptionLogService;
            _repository = repository;
        }
        public async Task<IEnumerable<OperationDto>> GetOperationsWithJobIdAsync(Guid jobId)
        {
            return await _repository.GetOperationsWithJobIdAsync(jobId);
        }
    }
}
