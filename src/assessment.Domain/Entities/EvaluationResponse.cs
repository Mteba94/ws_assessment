namespace assessment.Domain.Entities;

public class EvaluationResponse : BaseEntity
{
    public int QuestionId { get; set; }
    public int EvaluationId { get; set; }
    public double Score { get; set; }

    public Evaluation Evaluation { get; set; } = null!;
    public Question Question { get; set; } = null!;
}
