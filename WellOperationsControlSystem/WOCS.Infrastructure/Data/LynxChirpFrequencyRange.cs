using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxChirpFrequencyRange
{
    [Key]
    public int Id { get; set; }

    public byte Value { get; set; }

    public double StartFrequencyHz { get; set; }

    public double EndFrequencyHz { get; set; }

    [StringLength(256)]
    public string? Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    public bool Active { get; set; }

    public int Duration { get; set; }

    [InverseProperty("LynxChirpFrequencyRange")]
    public virtual ICollection<LynxAssemblyDevice> LynxAssemblyDevices { get; set; } = new List<LynxAssemblyDevice>();
}
