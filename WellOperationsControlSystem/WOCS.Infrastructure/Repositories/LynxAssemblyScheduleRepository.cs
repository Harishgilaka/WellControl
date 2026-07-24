using Microsoft.EntityFrameworkCore;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;

namespace WOCS.Infrastructure.Repositories
{
    public class LynxAssemblyScheduleRepository : ILynxAssemblyScheduleRepository
    {
        private readonly WocsContext _context;
        public LynxAssemblyScheduleRepository(WocsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LynxAssemblyScheduleActionBlockDto>> GetAllAsyncWith(Guid id)
        {
            var query = _context.LynxAssemblyScheduleActionBlocks
                .Where(x => x.AssemblyId == id)
                .AsNoTracking()
                .OrderBy(e => e.BlockNumber);

            return await query.Select(x => new LynxAssemblyScheduleActionBlockDto
            {
                Id = x.Id,
                AssemblyId = x.AssemblyId,
                AssemblyDeviceReferenceId = x.AssemblyDeviceReferenceId,
                BlockNumber = x.BlockNumber,
                Name = x.Name,
                TransmissionInterval = x.TransmissionInterval,
                Duration = x.Duration,
                DataInterval = x.DataInterval,
                NumberOfRepeats = x.NumberOfRepeats,
                RepeatIndefinetly = x.RepeatIndefinetly,
                DataFormat = x.DataFormat,
                TemperatureResolution = x.TemperatureResolution,
                PressureResolution = x.PressureResolution,
                ActionBlockTypeId = x.ActionBlockTypeId,
                IsProposal = x.IsProposal,
                ProposalFor = x.ProposalFor,
                StationPosition = x.Assembly.Position,
                ScheduleStartTime = x.Assembly.ScheduleStartTime,
            }).ToListAsync();
        }
    }
}
