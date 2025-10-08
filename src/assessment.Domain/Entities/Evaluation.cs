namespace assessment.Domain.Entities;

public class Evaluation : BaseEntity
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int ProfessorId { get; set; }
    public string? CommentText { get; set; }
    public string? Sentiment { get; set; }

    public Professor Professor { get; set; } = null!;
}
