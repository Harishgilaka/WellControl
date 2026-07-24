using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

[Index("AssemblyDeviceReferenceId", Name = "IX_AssemblyDeviceReferenceId")]
[Index("AssemblyId", Name = "IX_AssemblyId")]
public partial class LynxAssemblyScheduleActionBlock
{
    [Key]
    public Guid Id { get; set; }

    public Guid AssemblyId { get; set; }

    public Guid AssemblyDeviceReferenceId { get; set; }

    public int BlockNumber { get; set; }

    public string? Name { get; set; }

    public int TransmissionInterval { get; set; }

    public int Duration { get; set; }

    public int DataInterval { get; set; }

    public int NumberOfRepeats { get; set; }

    public bool RepeatIndefinetly { get; set; }

    public int DataFormat { get; set; }

    public int TemperatureResolution { get; set; }

    public int PressureResolution { get; set; }

    public int ActionBlockTypeId { get; set; }

    public bool IsProposal { get; set; }

    public Guid? ProposalFor { get; set; }

    [ForeignKey("AssemblyId")]
    [InverseProperty("LynxAssemblyScheduleActionBlocks")]
    public virtual LynxAssembly Assembly { get; set; } = null!;

    [ForeignKey("AssemblyDeviceReferenceId")]
    [InverseProperty("LynxAssemblyScheduleActionBlocks")]
    public virtual LynxAssemblyDevice AssemblyDeviceReference { get; set; } = null!;

    [InverseProperty("LynxAssemblyScheduleActionBlock")]
    public virtual ICollection<LynxAssemblyScheduleAction> LynxAssemblyScheduleActions { get; set; } = new List<LynxAssemblyScheduleAction>();
}
