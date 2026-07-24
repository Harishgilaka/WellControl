namespace WOCS.Domain.Entities
{
    public class StationDto
    {
        public StationDto()
        {
            Assemblies = new List<AssemblyDto>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Position { get; set; }
        public int PowerLevel { get; set; }
        public int DbLevel { get; set; }
        public Guid OperationVersionId { get; set; }
        public bool IsConfigured { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public IEnumerable<AssemblyDto> Assemblies { get; set; }
    }
}
