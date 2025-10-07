using assessment.Application.Abstractions.Messaging;

namespace assessment.Application.UseCase.Evaluations.Command.Create;

public class CreateEvaluationCommand : ICommand<bool>
{
    public int ProfessorId { get; set; }
    public string? Comments { get; set; } = null!;
    public ICollection<EvaluationResponseRequest> EvaluationResponseRequests { get; set; } = null!;
    public RatingsRequest RatingsRequest { get; set; } = null!;
}

public class EvaluationResponseRequest
{
    public int QuestionId { get; set; }
    public int Score { get; set; }
}

public class  RatingsRequest
{
    public double Teaching { get; set; }
    public double Clarity { get; set; }
    public double Availability { get; set; }
    public double Fairness { get; set; }
    public double Overall { get; set; }
}
