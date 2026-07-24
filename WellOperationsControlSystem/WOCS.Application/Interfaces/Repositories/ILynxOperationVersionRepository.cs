using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Repositories
{
    public interface ILynxOperationVersionRepository
    {
        Task<IEnumerable<OperationVersionDto>> GetAllOperationVersionsWithOperationIdAsync(Guid operationId);
    }
}
