using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("ClientId", Name = "IX_ClientId")]
public partial class ExproJob
{
    [Key]
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    [StringLength(256)]
    public string? ContactAddress { get; set; }

    [StringLength(256)]
    public string? ContactDetails { get; set; }

    [StringLength(256)]
    public string? ContactName { get; set; }

    [StringLength(256)]
    public string? ContactTelephone { get; set; }

    [StringLength(1024)]
    public string? Description { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string? ModifiedBy { get; set; }

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;
}
