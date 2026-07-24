using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxOperationType
{
    [Key]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    [InverseProperty("OperationType")]
    public virtual ICollection<LynxOperation> LynxOperations { get; set; } = new List<LynxOperation>();
}
