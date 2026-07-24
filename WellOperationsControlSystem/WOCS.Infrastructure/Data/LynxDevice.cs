using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("TypeId", Name = "IX_TypeId")]
public partial class LynxDevice
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string? BuildString { get; set; }

    public int TypeId { get; set; }

    [StringLength(10)]
    public string? FirmwareVersion { get; set; }

    [StringLength(32)]
    public string? LynxUID { get; set; }

    [MaxLength(500)]
    public byte[]? SensorCoefficients { get; set; }

    public int? SensorSampleInterval { get; set; }

    [InverseProperty("LynxDevice")]
    public virtual ICollection<LynxAssemblyDevice> LynxAssemblyDevices { get; set; } = new List<LynxAssemblyDevice>();

    [ForeignKey("TypeId")]
    [InverseProperty("LynxDevices")]
    public virtual LynxDeviceType Type { get; set; } = null!;
}
