using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxDeviceType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? DisplayName { get; set; }

    [InverseProperty("LynxDeviceType")]
    public virtual ICollection<LynxAssemblyDevice> LynxAssemblyDevices { get; set; } = new List<LynxAssemblyDevice>();

    [InverseProperty("DeviceType")]
    public virtual ICollection<LynxDeviceGroupDevice> LynxDeviceGroupDevices { get; set; } = new List<LynxDeviceGroupDevice>();

    [InverseProperty("Type")]
    public virtual ICollection<LynxDevice> LynxDevices { get; set; } = new List<LynxDevice>();
}
