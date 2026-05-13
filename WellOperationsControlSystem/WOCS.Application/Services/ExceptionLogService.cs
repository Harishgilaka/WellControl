using WOCS.Application.Interfaces.Repositories;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;

namespace WOCS.Application.Services
{
    public class ExceptionLogService : IExceptionLogService
    {
        private readonly IExceptionLogRepository _repository;
        //private ILogger<ExceptionLogService> _logger;
        public ExceptionLogService(IExceptionLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExceptionLogDto>> GetLogsAsync(int? count = null)
        {
            return await _repository.GetAllAsync(count);
        }

        public async Task LogAsync(Exception exception, string layer, string? context = null, string? contextData = null, bool isRecoverable = true)
        {
            try
            {
                var dto = new ExceptionLogDto
                {
                    Id = Guid.NewGuid(),
                    ErrorCode = "GEN-001",
                    Layer = layer,
                    ExceptionType = exception.GetType().FullName!,
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    InnerExceptionDetails = exception.InnerException?.ToString(),
                    Context = context,
                    ContextData = contextData,

                    UserName = Environment.UserName,
                    MachineName = Environment.MachineName,
                    ApplicationName = "WOCS",
                    ApplicationVersion = "1", //AppVersion.Current, // your helper
                    ProcessId = Environment.ProcessId,
                    ThreadId = Environment.CurrentManagedThreadId,

                    LogLevel = "Error",
                    IsRecoverable = isRecoverable,
                    ShouldTerminate = !isRecoverable,

                    OccurredAt = DateTime.UtcNow,
                    LoggedAt = DateTime.UtcNow,
                    CreatedBy = Environment.UserName,
                    CreatedDate = DateTime.UtcNow
                };

                await _repository.AddAsync(dto);
            }
            catch(Exception dbEx)
            {
                // DB is down — fall back to NLog only, never rethrow
                //_logger.LogError(dbEx, "ExceptionLog DB write failed — falling back to file log");
                //_logger.LogError(ex, "[ORIGINAL] Layer={Layer} Context={Context}", layer, context);
            }
        }
    }
}
