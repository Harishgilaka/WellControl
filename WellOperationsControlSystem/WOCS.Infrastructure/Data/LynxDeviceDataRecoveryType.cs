using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxDeviceDataRecoveryType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("LynxDeviceDataRecoveryType")]
    public virtual ICollection<LynxDeviceDataRecovery> LynxDeviceDataRecoveries { get; set; } = new List<LynxDeviceDataRecovery>();
}
