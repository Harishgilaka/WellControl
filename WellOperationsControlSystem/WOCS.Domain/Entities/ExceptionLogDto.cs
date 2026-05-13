namespace WOCS.Domain.Entities;

public class ExceptionLogDto
{
    public Guid Id { get; set; }

    public string ErrorCode { get; set; } = null!;

    public string Layer { get; set; } = null!;

    public string ExceptionType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? StackTrace { get; set; }

    public string? InnerExceptionDetails { get; set; }

    public string? Context { get; set; }

    public string? ContextData { get; set; }

    public string? UserName { get; set; }

    public string? MachineName { get; set; }

    public string? ApplicationVersion { get; set; }

    public string ApplicationName { get; set; } = null!;

    public int? ThreadId { get; set; }

    public int? ProcessId { get; set; }

    public bool IsRecoverable { get; set; }

    public bool ShouldTerminate { get; set; }

    public string LogLevel { get; set; } = null!;

    public DateTime OccurredAt { get; set; }

    public DateTime LoggedAt { get; set; }

    public bool IsReviewed { get; set; }

    public string? AdminNotes { get; set; }

    public string? RelatedEntityType { get; set; }

    public string? RelatedEntityId { get; set; }

    public string? CorrelationId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
