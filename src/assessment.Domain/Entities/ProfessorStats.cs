namespace assessment.Domain.Entities;

public class ProfessorStats : BaseEntity
{
    public int TotalResponses { get; set; }
    public double AverageRating { get; set; }
    public double CompletionRate { get; set; }
    public double SemesterAverage { get; set; }
    public int ProfessorId { get; set; }

    public Professor Professor { get; set; } = null!;
}
