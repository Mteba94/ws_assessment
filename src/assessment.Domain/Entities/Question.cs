namespace assessment.Domain.Entities;

public class Question : BaseEntity
{
    public string Key { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
