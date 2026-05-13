namespace WOCS.Domain.Entities;

public class ExproJobDto
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public string? ContactAddress { get; set; }

    public string? ContactDetails { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTelephone { get; set; }

    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? ModifiedBy { get; set; }

    public string CreatedBy { get; set; } = null!;
}
