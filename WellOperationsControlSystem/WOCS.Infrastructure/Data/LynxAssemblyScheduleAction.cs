using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("LynxAssemblySceduleActionTypeId", Name = "IX_LynxAssemblySceduleActionTypeId")]
[Index("LynxAssemblyScheduleActionBlockId", Name = "IX_LynxAssemblyScheduleActionBlockId")]
[Index("TargetAssemblyId", Name = "IX_TargetAssemblyId")]
[Index("TargetDeviceId", Name = "IX_TargetDeviceId")]
public partial class LynxAssemblyScheduleAction
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int LynxAssemblySceduleActionTypeId { get; set; }

    public Guid LynxAssemblyScheduleActionBlockId { get; set; }

    public Guid TargetAssemblyId { get; set; }

    public Guid TargetDeviceId { get; set; }

    public int? TargetAssemblyLocalId { get; set; }

    public int? TargetDeviceLocalId { get; set; }

    public int? TargetRouteNumber { get; set; }

    public int? IntervalType { get; set; }

    public double? Interval { get; set; }

    public int? ControlState { get; set; }

    public int? PowerLevel { get; set; }

    public int? DataFormat { get; set; }

    public int TemperatureResolution { get; set; }

    public int PressureResolution { get; set; }

    public bool CompressResponses { get; set; }

    public int? Resolution { get; set; }

    public int? TransmissionSpeedHint { get; set; }

    public double? Duration { get; set; }

    public int? WaitForDuration { get; set; }

    public int? OutputRouteNumber { get; set; }

    public bool IsActiveSchedule { get; set; }

    public int TimeOffsetInSeconds { get; set; }

    [StringLength(50)]
    public string? CommandString { get; set; }

    public int ActionIndex { get; set; }

    public int AssemblyActionIndex { get; set; }

    public int ActionBlock { get; set; }

    public int? NumberOfRepeats { get; set; }

    public int? ActionBlockToCall { get; set; }

    public bool UseMinutes { get; set; }

    [ForeignKey("LynxAssemblySceduleActionTypeId")]
    [InverseProperty("LynxAssemblyScheduleActions")]
    public virtual LynxAssemblySceduleActionType LynxAssemblySceduleActionType { get; set; } = null!;

    [ForeignKey("LynxAssemblyScheduleActionBlockId")]
    [InverseProperty("LynxAssemblyScheduleActions")]
    public virtual LynxAssemblyScheduleActionBlock LynxAssemblyScheduleActionBlock { get; set; } = null!;

    [ForeignKey("TargetAssemblyId")]
    [InverseProperty("LynxAssemblyScheduleActions")]
    public virtual LynxAssembly TargetAssembly { get; set; } = null!;

    [ForeignKey("TargetDeviceId")]
    [InverseProperty("LynxAssemblyScheduleActions")]
    public virtual LynxAssemblyDevice TargetDevice { get; set; } = null!;
}
