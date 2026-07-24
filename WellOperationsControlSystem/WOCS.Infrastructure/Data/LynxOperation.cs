using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("JobConfigurationId", Name = "IX_JobConfigurationId")]
[Index("OperationTypeId", Name = "IX_OperationTypeId")]
[Index("WellId", Name = "IX_WellId")]
public partial class LynxOperation
{
    [Key]
    public Guid Id { get; set; }

    public Guid JobConfigurationId { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [StringLength(1024)]
    public string? Notes { get; set; }

    public Guid WellId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string ModifiedBy { get; set; } = null!;

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;

    public int OperationTypeId { get; set; }

    [ForeignKey("JobConfigurationId")]
    [InverseProperty("LynxOperations")]
    public virtual ExproJob JobConfiguration { get; set; } = null!;

    [InverseProperty("Operation")]
    public virtual ICollection<LynxOperationVersion> LynxOperationVersions { get; set; } = new List<LynxOperationVersion>();

    [ForeignKey("OperationTypeId")]
    [InverseProperty("LynxOperations")]
    public virtual LynxOperationType OperationType { get; set; } = null!;

    [ForeignKey("WellId")]
    [InverseProperty("LynxOperations")]
    public virtual ExproWell Well { get; set; } = null!;
}
