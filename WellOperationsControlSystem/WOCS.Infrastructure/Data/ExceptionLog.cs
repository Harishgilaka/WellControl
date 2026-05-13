using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("ApplicationName", Name = "IX_ExceptionLogs_ApplicationName")]
[Index("CreatedBy", Name = "IX_ExceptionLogs_CreatedBy")]
[Index("CreatedDate", Name = "IX_ExceptionLogs_CreatedDate", AllDescending = true)]
[Index("ErrorCode", Name = "IX_ExceptionLogs_ErrorCode")]
[Index("Layer", Name = "IX_ExceptionLogs_Layer")]
[Index("OccurredAt", Name = "IX_ExceptionLogs_OccurredAt", AllDescending = true)]
[Index("UserName", Name = "IX_ExceptionLogs_UserName")]
public partial class ExceptionLog
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string ErrorCode { get; set; } = null!;

    [StringLength(50)]
    public string Layer { get; set; } = null!;

    [StringLength(256)]
    public string ExceptionType { get; set; } = null!;

    [StringLength(1024)]
    public string Message { get; set; } = null!;

    public string? StackTrace { get; set; }

    public string? InnerExceptionDetails { get; set; }

    [StringLength(512)]
    public string? Context { get; set; }

    public string? ContextData { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? MachineName { get; set; }

    [StringLength(50)]
    public string? ApplicationVersion { get; set; }

    [StringLength(256)]
    public string ApplicationName { get; set; } = null!;

    public int? ThreadId { get; set; }

    public int? ProcessId { get; set; }

    public bool IsRecoverable { get; set; }

    public bool ShouldTerminate { get; set; }

    [StringLength(50)]
    public string LogLevel { get; set; } = null!;

    public DateTime OccurredAt { get; set; }

    public DateTime LoggedAt { get; set; }

    public bool IsReviewed { get; set; }

    [StringLength(1024)]
    public string? AdminNotes { get; set; }

    [StringLength(256)]
    public string? RelatedEntityType { get; set; }

    [StringLength(256)]
    public string? RelatedEntityId { get; set; }

    [StringLength(256)]
    public string? CorrelationId { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
