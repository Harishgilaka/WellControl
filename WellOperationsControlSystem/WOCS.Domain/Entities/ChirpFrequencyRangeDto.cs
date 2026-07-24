namespace WOCS.Domain.Entities
{
    public class ChirpFrequencyRangeDto
    {
        public int Id { get; set; }
        public byte Value { get; set; }
        public double StartFrequencyHz { get; set; }
        public double EndFrequencyHz { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public int Duration { get; set; }
    }
}
