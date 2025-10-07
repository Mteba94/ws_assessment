using assessment.Application.Interfaces.Persistence;
using assessment.Domain.Entities;
using assessment.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infraestructure.Persistence.Repositories;

public class EvaluationResponseRepository(ApplicationDbContext _context) : GenericRepository<EvaluationResponse>(_context), IEvaluationResponseRepository
{
    private readonly ApplicationDbContext _context = _context;

    public async Task<IEnumerable<EvaluationResponse>> CountEvaluations(int professorId)
    {
        return await _context.EvaluationResponses
            .Include(er => er.Evaluation)
            .Where(er => er.Evaluation.ProfessorId == professorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<EvaluationResponse>> GetByProfessorIdAsync(int professorId)
    {
        return await _context.EvaluationResponses
            .Include(er => er.Evaluation)
            .Where(er => er.Evaluation.ProfessorId == professorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<EvaluationResponse>> GetByProfessorLast6MonthsAsync(int professorId)
    {
        var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);

        return await _context.EvaluationResponses
            .Include(er => er.Evaluation)
            .Where(er => er.Evaluation.ProfessorId == professorId && er.Evaluation.Date >= sixMonthsAgo)
            .ToListAsync();
    }

    public async Task<bool> RegistrarEvaluationResponse(IEnumerable<EvaluationResponse> evaluationResponses)
    {
        foreach(var evaluation in evaluationResponses)
        {
            evaluation.Status = 1;
            _context.EvaluationResponses.Add(evaluation);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }
}
