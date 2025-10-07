namespace assessment.Domain.Entities;

public class Professor : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Course { get; set; } = null!;
    public string? Image { get; set; }
}
