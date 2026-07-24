using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxLrcmTransmissionSpeed
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string Name { get; set; } = null!;

    public double Value { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("LynxLrcmTransmissionSpeed")]
    public virtual ICollection<LynxLink> LynxLinks { get; set; } = new List<LynxLink>();

    [InverseProperty("LynxLrcmTransmissionSpeed")]
    public virtual ICollection<LynxOperationVersion> LynxOperationVersions { get; set; } = new List<LynxOperationVersion>();
}
