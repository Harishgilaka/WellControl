using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxDeviceGroup
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string? Description { get; set; }

    [InverseProperty("Group")]
    public virtual ICollection<LynxDeviceGroupDevice> LynxDeviceGroupDevices { get; set; } = new List<LynxDeviceGroupDevice>();
}
