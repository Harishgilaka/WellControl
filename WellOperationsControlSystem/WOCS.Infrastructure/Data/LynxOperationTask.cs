using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("LynxDeviceDataRecoveryId", Name = "IX_LynxDeviceDataRecoveryId")]
[Index("OperationVersionId", Name = "IX_OperationVersionId")]
[Index("TargetDeviceId", Name = "IX_TargetDeviceId")]
[Index("TaskStatusId", Name = "IX_TaskStatusId")]
[Index("TaskTypeId", Name = "IX_TaskTypeId")]
public partial class LynxOperationTask
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int TaskStatusId { get; set; }

    public Guid OperationVersionId { get; set; }

    public Guid TargetDeviceId { get; set; }

    public int TaskTypeId { get; set; }

    public int DataInterval { get; set; }

    public int? NoOfLiveDataTransmitPeriods { get; set; }

    public int? LiveDataTransmitPeriod { get; set; }

    public int TaskDuration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ScheduledStartDateTime { get; set; }

    public int SortOrder { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EndDateTime { get; set; }

    public Guid LynxDeviceDataRecoveryId { get; set; }

    [ForeignKey("LynxDeviceDataRecoveryId")]
    [InverseProperty("LynxOperationTasks")]
    public virtual LynxDeviceDataRecovery LynxDeviceDataRecovery { get; set; } = null!;

    [ForeignKey("OperationVersionId")]
    [InverseProperty("LynxOperationTasks")]
    public virtual LynxOperationVersion OperationVersion { get; set; } = null!;

    [ForeignKey("TargetDeviceId")]
    [InverseProperty("LynxOperationTasks")]
    public virtual LynxAssemblyDevice TargetDevice { get; set; } = null!;

    [ForeignKey("TaskStatusId")]
    [InverseProperty("LynxOperationTasks")]
    public virtual LynxOperationTaskStatus TaskStatus { get; set; } = null!;

    [ForeignKey("TaskTypeId")]
    [InverseProperty("LynxOperationTasks")]
    public virtual LynxOperationTaskType TaskType { get; set; } = null!;
}
