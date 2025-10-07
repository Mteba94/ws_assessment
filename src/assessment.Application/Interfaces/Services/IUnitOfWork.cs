using assessment.Application.Interfaces.Persistence;
using assessment.Domain.Entities;
using System.Data;

namespace assessment.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Professor> Professor { get; }
    IGenericRepository<Question> Question { get; }
    IEvaluationResponseRepository EvaluationResponse { get; }
    IRatingRepository Ratings { get; }
    IProfessorStatsRepository ProfessorStats { get; }
    IEvaluationsRepository Evaluation { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction();
}
