using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("LynxFrequencyBandId", Name = "IX_LynxFrequencyBandId")]
[Index("LynxLrcmTransmissionSpeedId", Name = "IX_LynxLrcmTransmissionSpeedId")]
[Index("OperationId", Name = "IX_OperationId")]
public partial class LynxOperationVersion
{
    [Key]
    public Guid Id { get; set; }

    public Guid OperationId { get; set; }

    public int VersionId { get; set; }

    [StringLength(1024)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;

    public bool IsConfigured { get; set; }

    public int LynxFrequencyBandId { get; set; }

    public int LynxLrcmTransmissionSpeedId { get; set; }

    public int Depth { get; set; }

    public int AcousticAddress { get; set; }

    [StringLength(10)]
    public string? COMPortP0 { get; set; }

    [StringLength(10)]
    public string? COMPortP1 { get; set; }

    [InverseProperty("OperationVersion")]
    public virtual ICollection<LynxDeviceDataRecovery> LynxDeviceDataRecoveries { get; set; } = new List<LynxDeviceDataRecovery>();

    [ForeignKey("LynxFrequencyBandId")]
    [InverseProperty("LynxOperationVersions")]
    public virtual LynxFrequencyBand LynxFrequencyBand { get; set; } = null!;

    [InverseProperty("LynxOperationVersion")]
    public virtual ICollection<LynxLink> LynxLinks { get; set; } = new List<LynxLink>();

    [ForeignKey("LynxLrcmTransmissionSpeedId")]
    [InverseProperty("LynxOperationVersions")]
    public virtual LynxLrcmTransmissionSpeed LynxLrcmTransmissionSpeed { get; set; } = null!;

    [InverseProperty("OperationVersion")]
    public virtual ICollection<LynxOperationTask> LynxOperationTasks { get; set; } = new List<LynxOperationTask>();

    [InverseProperty("OperationVersion")]
    public virtual ICollection<LynxRouteSegment> LynxRouteSegments { get; set; } = new List<LynxRouteSegment>();

    [InverseProperty("OperationVersion")]
    public virtual ICollection<LynxStation> LynxStations { get; set; } = new List<LynxStation>();

    [ForeignKey("OperationId")]
    [InverseProperty("LynxOperationVersions")]
    public virtual LynxOperation Operation { get; set; } = null!;
}
