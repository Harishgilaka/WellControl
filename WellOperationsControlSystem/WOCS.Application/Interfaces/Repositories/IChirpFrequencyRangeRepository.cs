using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Repositories
{
    public interface IChirpFrequencyRangeRepository
    {
        Task<IEnumerable<ChirpFrequencyRangeDto>> GetAllAsync();
    }
}
