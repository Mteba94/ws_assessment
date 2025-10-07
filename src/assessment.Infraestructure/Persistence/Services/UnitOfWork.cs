using assessment.Application.Interfaces.Persistence;
using assessment.Application.Interfaces.Services;
using assessment.Domain.Entities;
using assessment.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace assessment.Infraestructure.Persistence.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IGenericRepository<Professor> Professor { get; }
    public IGenericRepository<Question> Question { get; }
    public IEvaluationResponseRepository EvaluationResponse { get; }
    public IRatingRepository Ratings { get; }
    public IProfessorStatsRepository ProfessorStats { get; }
    public IEvaluationsRepository Evaluation { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IGenericRepository<Professor> professor,
        IGenericRepository<Question> question,
        IEvaluationResponseRepository evaluationResponse,
        IRatingRepository ratings,
        IProfessorStatsRepository professorStats,
        IEvaluationsRepository evaluation)
    {
        _context = context;
        Professor = professor;
        Question = question;
        EvaluationResponse = evaluationResponse;
        Ratings = ratings;
        ProfessorStats = professorStats;
        Evaluation = evaluation;
    }

    public IDbTransaction BeginTransaction() =>
            _context.Database.BeginTransaction().GetDbTransaction();

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);

    public void Dispose() => _context.Dispose();
}
