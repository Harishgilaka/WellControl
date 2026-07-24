using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("LynxFrequencyBandId", Name = "IX_LynxFrequencyBandId")]
public partial class LynxLinkPowerLevel
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public int LynxFrequencyBandId { get; set; }

    public double Value { get; set; }

    [ForeignKey("LynxFrequencyBandId")]
    [InverseProperty("LynxLinkPowerLevels")]
    public virtual LynxFrequencyBand LynxFrequencyBand { get; set; } = null!;
}
