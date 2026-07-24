using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class ExproClient
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;

    [InverseProperty("Client")]
    public virtual ICollection<ExproJob> ExproJobs { get; set; } = new List<ExproJob>();
}
