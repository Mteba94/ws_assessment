using assessment.Application.Interfaces.Persistence;
using assessment.Domain.Entities;
using assessment.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infraestructure.Persistence.Repositories;

public class EvaluationsRepository(ApplicationDbContext _context) : GenericRepository<Evaluation>(_context), IEvaluationsRepository
{
    public async Task<Evaluation> GetByProfessorId(int professorId)
    {
        var evaluation = await _context.Evaluations
            .Where(e => e.ProfessorId == professorId)
            .FirstOrDefaultAsync();

        return evaluation!;
    }
}
