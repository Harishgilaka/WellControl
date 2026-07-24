using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("OperationVersionId", Name = "IX_OperationVersionId")]
public partial class LynxStation
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; } = null!;

    public int Position { get; set; }

    public int PowerLevel { get; set; }

    public int DbLevel { get; set; }

    public Guid OperationVersionId { get; set; }

    public bool IsConfigured { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string? CreatedBy { get; set; }

    [InverseProperty("Station")]
    public virtual ICollection<LynxAssembly> LynxAssemblies { get; set; } = new List<LynxAssembly>();

    [ForeignKey("OperationVersionId")]
    [InverseProperty("LynxStations")]
    public virtual LynxOperationVersion OperationVersion { get; set; } = null!;
}
