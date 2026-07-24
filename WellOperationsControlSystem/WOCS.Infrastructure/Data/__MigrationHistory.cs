using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[PrimaryKey("MigrationId", "ContextKey")]
[Table("__MigrationHistory")]
public partial class __MigrationHistory
{
    [Key]
    [StringLength(150)]
    public string MigrationId { get; set; } = null!;

    [Key]
    [StringLength(300)]
    public string ContextKey { get; set; } = null!;

    public byte[] Model { get; set; } = null!;

    [StringLength(32)]
    public string ProductVersion { get; set; } = null!;
}
