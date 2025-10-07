using assessment.Application.UseCase.Evaluations.Command.Create;
using assessment.Domain.Entities;
using Mapster;

namespace assessment.Application.Mappings;

public class EvaluationMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateEvaluationCommand, Evaluation>()
            .Map(dest => dest.CommentText, src => src.Comments)
            .TwoWays();
    }
}
