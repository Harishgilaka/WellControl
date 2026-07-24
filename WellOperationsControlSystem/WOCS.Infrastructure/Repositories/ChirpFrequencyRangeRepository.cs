using Microsoft.EntityFrameworkCore;
using WOCS.Application.Interfaces.Repositories;
using WOCS.Domain.Entities;
using WOCS.Infrastructure.Data;

namespace WOCS.Infrastructure.Repositories
{
    public class ChirpFrequencyRangeRepository : IChirpFrequencyRangeRepository
    {
        private readonly WocsContext _context;
        public ChirpFrequencyRangeRepository(WocsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ChirpFrequencyRangeDto>> GetAllAsync()
        {
            return await _context.LynxChirpFrequencyRanges
                .Select(cfr => new ChirpFrequencyRangeDto
                {
                    Id = cfr.Id,
                    Value = cfr.Value,
                    StartFrequencyHz = cfr.StartFrequencyHz,
                    EndFrequencyHz = cfr.EndFrequencyHz,
                    Name = cfr.Name,
                    Description = cfr.Description,
                    Active = cfr.Active,
                    Duration = cfr.Duration
                })
                .ToListAsync();
        }
    }
}
