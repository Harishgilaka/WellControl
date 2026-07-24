using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;

namespace WOCS.Application.Services
{
    public class ChirpFrequencyRangeService : IChirpFrequencyRangeService
    {
        private readonly IChirpFrequencyRangeRepository _chirpFrequencyRangeRepository;
        private readonly IExceptionLogService _exceptionLogService;

        public ChirpFrequencyRangeService(IChirpFrequencyRangeRepository chirpFrequencyRangeRepository, IExceptionLogService exceptionLogService)
        {
            _chirpFrequencyRangeRepository = chirpFrequencyRangeRepository;
            _exceptionLogService = exceptionLogService;
        }
        public async Task<IEnumerable<ChirpFrequencyRangeDto>> GetAllChirpFrequencyRangeAsync()
        {

            try
            {
                var chirpFrequencyRanges = await _chirpFrequencyRangeRepository.GetAllAsync();
                return chirpFrequencyRanges;
            }
            catch (Exception ex)
            {
                await _exceptionLogService.LogAsync(ex, layer: "Application", context: "Getting all Chirp Frequency Ranges");
                throw;
            }
        }
    }
}
