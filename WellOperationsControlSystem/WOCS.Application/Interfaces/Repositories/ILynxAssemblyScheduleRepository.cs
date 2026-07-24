using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Repositories
{
    public interface ILynxAssemblyScheduleRepository
    {
        Task<IEnumerable<LynxAssemblyScheduleActionBlockDto>> GetAllAsyncWith(Guid id);
    }
}
