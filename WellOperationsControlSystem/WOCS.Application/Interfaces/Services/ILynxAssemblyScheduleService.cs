using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Services
{
    public interface ILynxAssemblyScheduleService
    {
        Task<IEnumerable<LynxAssemblyScheduleActionBlockDto>> GetActionBlockByIdAsync(Guid id);
    }
}
