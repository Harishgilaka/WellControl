using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("OperationVersionId", Name = "IX_OperationVersionId")]
[Index("StartAssemblyId", Name = "IX_StartAssemblyId")]
[Index("TargetAssemblyId", Name = "IX_TargetAssemblyId")]
public partial class LynxRouteSegment
{
    [Key]
    public Guid Id { get; set; }

    public Guid OperationVersionId { get; set; }

    public int RouteId { get; set; }

    public int Direction { get; set; }

    public bool IsEndpoint { get; set; }

    public Guid StartAssemblyId { get; set; }

    public Guid TargetAssemblyId { get; set; }

    public int PowerLevel { get; set; }

    [ForeignKey("OperationVersionId")]
    [InverseProperty("LynxRouteSegments")]
    public virtual LynxOperationVersion OperationVersion { get; set; } = null!;

    [ForeignKey("StartAssemblyId")]
    [InverseProperty("LynxRouteSegmentStartAssemblies")]
    public virtual LynxAssembly StartAssembly { get; set; } = null!;

    [ForeignKey("TargetAssemblyId")]
    [InverseProperty("LynxRouteSegmentTargetAssemblies")]
    public virtual LynxAssembly TargetAssembly { get; set; } = null!;
}
