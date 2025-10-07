namespace assessment.Domain.Entities;

public class Ratings : BaseEntity
{
    public double Teaching { get; set; }
    public double Clarity { get; set; }
    public double Availability { get; set; }
    public double Fairness { get; set; }
    public double Overall { get; set; }
    public int ProfessorId { get; set; }

    public Professor Professor { get; set; } = null!;
}
