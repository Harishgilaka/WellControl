using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("FluidTypeId", Name = "IX_FluidTypeId")]
[Index("WellTypeId", Name = "IX_WellTypeId")]
public partial class ExproWell
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string? Field { get; set; }

    [StringLength(256)]
    public string? Location { get; set; }

    [StringLength(256)]
    public string? Coordinates { get; set; }

    [StringLength(256)]
    public string? Depth { get; set; }

    [StringLength(256)]
    public string? H2S { get; set; }

    [StringLength(256)]
    public string? CO2 { get; set; }

    [StringLength(256)]
    public string? WaterDepth { get; set; }

    public int WellTypeId { get; set; }

    public int FluidTypeId { get; set; }

    public string TimeZoneId { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;

    [ForeignKey("FluidTypeId")]
    [InverseProperty("ExproWells")]
    public virtual ExproFluidType FluidType { get; set; } = null!;

    [InverseProperty("Well")]
    public virtual ICollection<LynxOperation> LynxOperations { get; set; } = new List<LynxOperation>();

    [ForeignKey("WellTypeId")]
    [InverseProperty("ExproWells")]
    public virtual ExproWellType WellType { get; set; } = null!;

    [ForeignKey("ExproWell_Id")]
    [InverseProperty("ExproWells")]
    public virtual ICollection<ExproJob> ExproJobs { get; set; } = new List<ExproJob>();
}
