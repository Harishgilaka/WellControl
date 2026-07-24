using WOCS.Domain.Enums;

namespace WOCS.Domain.Entities
{
    public class ExproWellDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public int WellTypeId { get; set; }
        public ExproWellType WellType { get; set; }
        public int FluidTypeId { get; set; }
        public ExproFluidType FluidType { get; set; }
        public string TimeZoneId { get; set; } = null!;
    }
}
