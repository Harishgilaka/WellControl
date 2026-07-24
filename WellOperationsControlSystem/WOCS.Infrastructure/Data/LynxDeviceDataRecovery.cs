using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("AssemblyDeviceId", Name = "IX_AssemblyDeviceId")]
[Index("LynxDeviceDataRecoveryTypeId", Name = "IX_LynxDeviceDataRecoveryTypeId")]
[Index("OperationVersionId", Name = "IX_OperationVersionId")]
public partial class LynxDeviceDataRecovery
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int LynxDeviceDataRecoveryTypeId { get; set; }

    public Guid OperationVersionId { get; set; }

    public int TemperaturePrecision { get; set; }

    public int PressurePrecision { get; set; }

    public bool ConvertData { get; set; }

    public Guid AssemblyDeviceId { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public DateTime? CirrusSyncDateTime { get; set; }

    [StringLength(260)]
    public string? CsvFilePath { get; set; }

    [ForeignKey("AssemblyDeviceId")]
    [InverseProperty("LynxDeviceDataRecoveries")]
    public virtual LynxAssemblyDevice AssemblyDevice { get; set; } = null!;

    [InverseProperty("LynxDeviceDataRecovery")]
    public virtual ICollection<LynxDeviceDataReading> LynxDeviceDataReadings { get; set; } = new List<LynxDeviceDataReading>();

    [ForeignKey("LynxDeviceDataRecoveryTypeId")]
    [InverseProperty("LynxDeviceDataRecoveries")]
    public virtual LynxDeviceDataRecoveryType LynxDeviceDataRecoveryType { get; set; } = null!;

    [InverseProperty("LynxDeviceDataRecovery")]
    public virtual ICollection<LynxOperationTask> LynxOperationTasks { get; set; } = new List<LynxOperationTask>();

    [ForeignKey("OperationVersionId")]
    [InverseProperty("LynxDeviceDataRecoveries")]
    public virtual LynxOperationVersion OperationVersion { get; set; } = null!;
}
