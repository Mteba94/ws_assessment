using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Dtos.Professor;

namespace assessment.Application.UseCase.Professors.Queries.GetAll;

public sealed class GetAllProfessorQuery : BaseFilters, IQuery<IEnumerable<ProfessorResponseDTO>>
{
}
