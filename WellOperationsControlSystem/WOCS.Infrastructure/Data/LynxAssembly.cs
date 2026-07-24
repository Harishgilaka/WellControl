using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("StationId", Name = "IX_StationId")]
public partial class LynxAssembly
{
    [Key]
    public Guid Id { get; set; }

    public int AssemblyId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public Guid StationId { get; set; }

    public int Position { get; set; }

    [StringLength(50)]
    public string ClassName { get; set; } = null!;

    public bool IsConfigured { get; set; }

    public int? LynxDeviceGroup { get; set; }

    public DateTime ScheduleStartTime { get; set; }

    public DateTime? ProposedScheduleStartTime { get; set; }

    public DateTime? ProposedScheduleUpdateSent { get; set; }

    public DateTime? ProposedScheduleUpdateAkLastQuery { get; set; }

    public int? ProposedScheduleUpdateResponseEta { get; set; }

    public int? ProposedScheduleUpdateMessageSize { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProposedScheduleUpdateAckReceived { get; set; }

    public int? ProposedScheduleUpdateAckStatus { get; set; }

    public DateTime? ScheduleEndTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string? CreatedBy { get; set; }

    [InverseProperty("Assembly")]
    public virtual ICollection<LynxAssemblyDevice> LynxAssemblyDevices { get; set; } = new List<LynxAssemblyDevice>();

    [InverseProperty("Assembly")]
    public virtual ICollection<LynxAssemblyScheduleActionBlock> LynxAssemblyScheduleActionBlocks { get; set; } = new List<LynxAssemblyScheduleActionBlock>();

    [InverseProperty("TargetAssembly")]
    public virtual ICollection<LynxAssemblyScheduleAction> LynxAssemblyScheduleActions { get; set; } = new List<LynxAssemblyScheduleAction>();

    [InverseProperty("FirstAssembly")]
    public virtual ICollection<LynxLink> LynxLinkFirstAssemblies { get; set; } = new List<LynxLink>();

    [InverseProperty("SecondAssembly")]
    public virtual ICollection<LynxLink> LynxLinkSecondAssemblies { get; set; } = new List<LynxLink>();

    [InverseProperty("StartAssembly")]
    public virtual ICollection<LynxRouteSegment> LynxRouteSegmentStartAssemblies { get; set; } = new List<LynxRouteSegment>();

    [InverseProperty("TargetAssembly")]
    public virtual ICollection<LynxRouteSegment> LynxRouteSegmentTargetAssemblies { get; set; } = new List<LynxRouteSegment>();

    [ForeignKey("StationId")]
    [InverseProperty("LynxAssemblies")]
    public virtual LynxStation Station { get; set; } = null!;
}
