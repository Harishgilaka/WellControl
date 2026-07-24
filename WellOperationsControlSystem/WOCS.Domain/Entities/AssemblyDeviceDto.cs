namespace WOCS.Domain.Entities
{
    public class AssemblyDeviceDto
    {
        public Guid Id { get; set; }
        public Guid AssemblyId { get; set; }
        public Guid? LynxDeviceId { get; set; }
        public int LynxDeviceTypeId { get; set; }
        public int? LynxLocalId { get; set; }
        public bool IsConfigured { get; set; }
        public string? FirmwareVersion { get; set; }
        public int? SamplingRateMS { get; set; }
        public int? FrequencyBandId { get; set; }
        public int? LynxChirpFrequencyRangeId { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
    }
}
