using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Services
{
    public interface ILynxOperationService
    {
       Task<IEnumerable<OperationDto>> GetOperationsWithJobIdAsync(Guid jobId);
    }
}
