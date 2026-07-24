using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxFrequencyBand
{
    [Key]
    public int Id { get; set; }

    public double LowBin { get; set; }

    public double HighBin { get; set; }

    public double LowFrequency { get; set; }

    public double HighFrequency { get; set; }

    [StringLength(250)]
    public string DisplayName { get; set; } = null!;

    [InverseProperty("LynxFrequencyBand")]
    public virtual ICollection<LynxLinkPowerLevel> LynxLinkPowerLevels { get; set; } = new List<LynxLinkPowerLevel>();

    [InverseProperty("LynxFrequencyBand")]
    public virtual ICollection<LynxOperationVersion> LynxOperationVersions { get; set; } = new List<LynxOperationVersion>();
}
