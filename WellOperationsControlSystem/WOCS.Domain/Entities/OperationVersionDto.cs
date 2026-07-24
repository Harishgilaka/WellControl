namespace WOCS.Domain.Entities
{
    public class OperationVersionDto
    {
        public OperationVersionDto()
        {
            Stations = new List<StationDto>();
        }
        public Guid Id { get; set; }
        public Guid OperationId { get; set; }
        public int VersionId { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool IsConfigured { get; set; }
        public int LynxFrequencyBandId { get; set; }
        public int LynxLrcmTransmissionSpeedId { get; set; }
        public int Depth { get; set; }
        public int AcousticAddress { get; set; }
        public string? COMPortP0 { get; set; }
        public string? COMPortP1 { get; set; }
        public IEnumerable<StationDto> Stations { get; set; }
    }
}
