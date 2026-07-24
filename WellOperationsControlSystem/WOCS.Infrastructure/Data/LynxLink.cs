using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("FirstAssemblyId", Name = "IX_FirstAssemblyId")]
[Index("LynxLrcmTransmissionSpeedId", Name = "IX_LynxLrcmTransmissionSpeedId")]
[Index("LynxOperationVersionId", Name = "IX_LynxOperationVersionId")]
[Index("SecondAssemblyId", Name = "IX_SecondAssemblyId")]
public partial class LynxLink
{
    [Key]
    public Guid Id { get; set; }

    public Guid? LynxOperationVersionId { get; set; }

    public Guid SecondAssemblyId { get; set; }

    public Guid FirstAssemblyId { get; set; }

    public int SignalIntegrity { get; set; }

    public int PowerLevelValue { get; set; }

    public int LynxLrcmTransmissionSpeedId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastSuccessfulPing { get; set; }

    [ForeignKey("FirstAssemblyId")]
    [InverseProperty("LynxLinkFirstAssemblies")]
    public virtual LynxAssembly FirstAssembly { get; set; } = null!;

    [ForeignKey("LynxLrcmTransmissionSpeedId")]
    [InverseProperty("LynxLinks")]
    public virtual LynxLrcmTransmissionSpeed LynxLrcmTransmissionSpeed { get; set; } = null!;

    [ForeignKey("LynxOperationVersionId")]
    [InverseProperty("LynxLinks")]
    public virtual LynxOperationVersion? LynxOperationVersion { get; set; }

    [ForeignKey("SecondAssemblyId")]
    [InverseProperty("LynxLinkSecondAssemblies")]
    public virtual LynxAssembly SecondAssembly { get; set; } = null!;
}
