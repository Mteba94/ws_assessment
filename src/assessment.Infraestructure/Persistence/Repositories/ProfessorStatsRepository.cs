using assessment.Application.Interfaces.Persistence;
using assessment.Domain.Entities;
using assessment.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infraestructure.Persistence.Repositories;

public class ProfessorStatsRepository(ApplicationDbContext _context) : GenericRepository<ProfessorStats>(_context), IProfessorStatsRepository
{
    public Task<ProfessorStats> GetByProfessorId(int professorId)
    {
        var professorStats = _context.ProfessorStats
            .Where(ps => ps.ProfessorId == professorId)
            .FirstOrDefaultAsync();

        return professorStats!;
    }
}
