using assessment.Application.Abstractions.Messaging;
using assessment.Application.Dtos.Dashboard;

namespace assessment.Application.UseCase.Professors.Queries.Dashboard;

public sealed class DashboardProfessorQuery : IQuery<ProfessorDashboardDTO>
{
    public int ProfessorId { get; set; }
}
