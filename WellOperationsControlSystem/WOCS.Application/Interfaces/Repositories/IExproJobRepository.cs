using System.Linq.Expressions;
using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Repositories
{
    public interface IExproJobRepository
    {
        Task<IEnumerable<ExproJobDto>> GetAllAsync(int? count = null);
        Task<IEnumerable<ExproJobDto>> FindAsync(Expression<Func<ExproJobDto, bool>> predicate);
    }
}
