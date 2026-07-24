using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class ExproFluidType
{
    [Key]
    public int Id { get; set; }

    public bool Active { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [InverseProperty("FluidType")]
    public virtual ICollection<ExproWell> ExproWells { get; set; } = new List<ExproWell>();
}
