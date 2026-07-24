using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("DeviceTypeId", Name = "IX_DeviceTypeId")]
[Index("GroupId", Name = "IX_GroupId")]
public partial class LynxDeviceGroupDevice
{
    [Key]
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int DeviceTypeId { get; set; }

    [ForeignKey("DeviceTypeId")]
    [InverseProperty("LynxDeviceGroupDevices")]
    public virtual LynxDeviceType DeviceType { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("LynxDeviceGroupDevices")]
    public virtual LynxDeviceGroup Group { get; set; } = null!;
}
