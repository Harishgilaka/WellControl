using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxAssemblySceduleActionType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public bool CanStartActionBlock { get; set; }

    public bool CanEndActionBlock { get; set; }

    [InverseProperty("LynxAssemblySceduleActionType")]
    public virtual ICollection<LynxAssemblyScheduleAction> LynxAssemblyScheduleActions { get; set; } = new List<LynxAssemblyScheduleAction>();
}
