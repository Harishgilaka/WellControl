namespace WOCS.Domain.Entities;

public class ExproJobDto
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Name { get; set; } = null!;
    public string ClientName { get; set; } = null!;
    public string? ContactAddress { get; set; }
    public bool IsActive { get; set; }
}
