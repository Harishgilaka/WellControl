using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxOperationTaskStatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;

    [InverseProperty("TaskStatus")]
    public virtual ICollection<LynxOperationTask> LynxOperationTasks { get; set; } = new List<LynxOperationTask>();
}
