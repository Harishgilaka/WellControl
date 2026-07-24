using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class LynxConfigurationDocument
{
    [Key]
    public Guid Id { get; set; }

    public Guid JobId { get; set; }

    public string? Name { get; set; }

    public int Version { get; set; }

    public string? ConfigurationDocumentData { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    [StringLength(256)]
    public string ModifiedBy { get; set; } = null!;

    [StringLength(256)]
    public string CreatedBy { get; set; } = null!;
}
