using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Services
{
    public interface ILynxOperationVersionService
    {
        Task<IEnumerable<OperationVersionDto>> GetAllOperationVersionsWithOperationIdAsync(Guid operationId);
    }
}
