using assessment.Application.Interfaces.Persistence;
using assessment.Domain.Entities;
using assessment.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infraestructure.Persistence.Repositories;

public class RatingRepository(ApplicationDbContext _context) : GenericRepository<Ratings>(_context), IRatingRepository
{
    private readonly ApplicationDbContext _context = _context;
    public async Task<Ratings> GetByProfessorId(int professorId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.ProfessorId == professorId)
            .FirstOrDefaultAsync();

        return ratings!;
    }
}
