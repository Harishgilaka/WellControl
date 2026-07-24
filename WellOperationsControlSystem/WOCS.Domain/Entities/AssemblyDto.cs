namespace WOCS.Domain.Entities
{
    public class AssemblyDto
    {
        public AssemblyDto()
        {
            AssemblyDevices = new List<AssemblyDeviceDto>();
            lynxAssemblyScheduleActionBlock = new List<LynxAssemblyScheduleActionBlockDto>();
        }
        public Guid Id { get; set; }
        public int AssemblyId { get; set; }
        public string Name { get; set; } = null!;
        public Guid StationId { get; set; }
        public int Position { get; set; }
        public string ClassName { get; set; } = null!;
        public bool IsConfigured { get; set; }
        public int? LynxDeviceGroup { get; set; }
        public DateTime ScheduleStartTime { get; set; }
        public DateTime? ProposedScheduleStartTime { get; set; }
        public DateTime? ProposedScheduleUpdateSent { get; set; }
        public DateTime? ProposedScheduleUpdateAkLastQuery { get; set; }
        public int? ProposedScheduleUpdateResponseEta { get; set; }
        public int? ProposedScheduleUpdateMessageSize { get; set; }
        public DateTime? ProposedScheduleUpdateAckReceived { get; set; }
        public int? ProposedScheduleUpdateAckStatus { get; set; }
        public DateTime? ScheduleEndTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public IEnumerable<AssemblyDeviceDto> AssemblyDevices { get; set; }
        public IEnumerable<LynxAssemblyScheduleActionBlockDto> lynxAssemblyScheduleActionBlock { get; set; }
    }
}
