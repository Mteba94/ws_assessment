using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Dtos.Question;

namespace assessment.Application.UseCase.Questions.Queries.GetAll;

public sealed class GetAllQuestionQuery : BaseFilters, IQuery<IEnumerable<QuestionResponseDTO>>
{
}
