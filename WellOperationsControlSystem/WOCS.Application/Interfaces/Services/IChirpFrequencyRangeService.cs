using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Services
{
    public interface IChirpFrequencyRangeService
    {
        Task<IEnumerable<ChirpFrequencyRangeDto>> GetAllChirpFrequencyRangeAsync();
    }
}
