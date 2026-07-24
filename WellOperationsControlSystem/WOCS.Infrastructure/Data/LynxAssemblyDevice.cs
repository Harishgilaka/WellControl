using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("AssemblyId", Name = "IX_AssemblyId")]
[Index("LynxChirpFrequencyRangeId", Name = "IX_LynxChirpFrequencyRangeId")]
[Index("LynxDeviceId", Name = "IX_LynxDeviceId")]
[Index("LynxDeviceTypeId", Name = "IX_LynxDeviceTypeId")]
public partial class LynxAssemblyDevice
{
    [Key]
    public Guid Id { get; set; }

    public Guid AssemblyId { get; set; }

    public Guid? LynxDeviceId { get; set; }

    public int LynxDeviceTypeId { get; set; }

    public int? LynxLocalId { get; set; }

    public bool IsConfigured { get; set; }

    [StringLength(10)]
    public string? FirmwareVersion { get; set; }

    public int? SamplingRateMS { get; set; }

    public int? FrequencyBandId { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string? CreatedBy { get; set; }

    public int? LynxChirpFrequencyRangeId { get; set; }

    [ForeignKey("AssemblyId")]
    [InverseProperty("LynxAssemblyDevices")]
    public virtual LynxAssembly Assembly { get; set; } = null!;

    [InverseProperty("AssemblyDeviceReference")]
    public virtual ICollection<LynxAssemblyScheduleActionBlock> LynxAssemblyScheduleActionBlocks { get; set; } = new List<LynxAssemblyScheduleActionBlock>();

    [InverseProperty("TargetDevice")]
    public virtual ICollection<LynxAssemblyScheduleAction> LynxAssemblyScheduleActions { get; set; } = new List<LynxAssemblyScheduleAction>();

    [ForeignKey("LynxChirpFrequencyRangeId")]
    [InverseProperty("LynxAssemblyDevices")]
    public virtual LynxChirpFrequencyRange? LynxChirpFrequencyRange { get; set; }

    [ForeignKey("LynxDeviceId")]
    [InverseProperty("LynxAssemblyDevices")]
    public virtual LynxDevice? LynxDevice { get; set; }

    [InverseProperty("AssemblyDevice")]
    public virtual ICollection<LynxDeviceDataRecovery> LynxDeviceDataRecoveries { get; set; } = new List<LynxDeviceDataRecovery>();

    [ForeignKey("LynxDeviceTypeId")]
    [InverseProperty("LynxAssemblyDevices")]
    public virtual LynxDeviceType LynxDeviceType { get; set; } = null!;

    [InverseProperty("TargetDevice")]
    public virtual ICollection<LynxOperationTask> LynxOperationTasks { get; set; } = new List<LynxOperationTask>();
}
