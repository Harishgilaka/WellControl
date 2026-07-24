using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("LynxDeviceDataRecoveryId", Name = "IX_LynxDeviceDataRecoveryId")]
[Index("ReadingTimeUTC", "AssemblyDeviceId", Name = "IX_ReadingTimeUTC_AssemblyDeviceId")]
public partial class LynxDeviceDataReading
{
    [Key]
    public Guid Id { get; set; }

    public Guid LynxDeviceDataRecoveryId { get; set; }

    public DateTime ReadingTimeUTC { get; set; }

    public Guid AssemblyDeviceId { get; set; }

    public long ReadingTimeTicks { get; set; }

    public DateTime ReadingTimeLocal { get; set; }

    public bool IsSynced { get; set; }

    public DateTime? Synced { get; set; }

    public double? TemperatureValue { get; set; }

    public double? PressureValue { get; set; }

    [ForeignKey("LynxDeviceDataRecoveryId")]
    [InverseProperty("LynxDeviceDataReadings")]
    public virtual LynxDeviceDataRecovery LynxDeviceDataRecovery { get; set; } = null!;
}
