using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxTimingMetric
{
    [Key]
    public Guid Id { get; set; }

    public Guid LynxOperationId { get; set; }

    [StringLength(256)]
    public string? ActionName { get; set; }

    public TimeOnly ExecutionTime { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string ModifiedBy { get; set; } = null!;

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;
}
