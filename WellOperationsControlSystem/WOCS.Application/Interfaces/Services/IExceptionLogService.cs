using WOCS.Domain.Entities;

namespace WOCS.Application.Interfaces.Services
{
    public interface IExceptionLogService
    {
        Task LogAsync(
            Exception exception,
            string layer,
            string? context = null,
            string? contextData = null,
            bool isRecoverable = true
        );

        Task<IEnumerable<ExceptionLogDto>> GetLogsAsync(int? count = null);
    }
}
