using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;

namespace WOCS.Application.Services
{
    public class LynxOperationVersionService : ILynxOperationVersionService
    {
        public readonly ILynxOperationVersionRepository _repository;

        public LynxOperationVersionService(ILynxOperationVersionRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<OperationVersionDto>> GetAllOperationVersionsWithOperationIdAsync(Guid operationId)
        {
            return _repository.GetAllOperationVersionsWithOperationIdAsync(operationId);
        }
    }
}
