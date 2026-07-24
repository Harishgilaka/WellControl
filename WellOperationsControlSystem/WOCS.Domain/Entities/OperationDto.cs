using WOCS.Domain.Enums;

namespace WOCS.Domain.Entities
{
    public class OperationDto
    {
        public Guid Id { get; set; }
        public Guid JobConfigurationId { get; set; }
        public string Name { get; set; } = null!;
        public Guid WellId { get; set; }
        public bool IsActive { get; set; }
        public int OperationTypeId { get; set; }
        public LynxOperationType OperationType { get; set; }
        public ExproWellDto ExproWell { get; set; } = new ExproWellDto();
    }
}
