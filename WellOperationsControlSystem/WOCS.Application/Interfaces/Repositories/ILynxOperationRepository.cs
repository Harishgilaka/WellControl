using System.Linq.Expressions;
using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Repositories
{
    public interface ILynxOperationRepository
    {
        Task<IEnumerable<OperationDto>> GetAllAsync(int? count = null);
        Task<IEnumerable<OperationDto>> FindAsync(Expression<Func<OperationDto, bool>> predicate);
        Task<IEnumerable<OperationDto>> GetOperationsWithJobIdAsync(Guid jobId);
    }
}
