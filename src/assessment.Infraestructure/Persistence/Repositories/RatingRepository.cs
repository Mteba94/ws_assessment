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
            .ToListAsync();

        var averageTeaching = ratings.Any() ? Math.Round(ratings.Average(r => r.Teaching), 2) : 0;
        var averageClarity = ratings.Any() ? Math.Round(ratings.Average(r => r.Clarity), 2) : 0;
        var averageAvailability = ratings.Any() ? Math.Round(ratings.Average(r => r.Availability), 2) : 0;
        var averageFairness = ratings.Any() ? Math.Round(ratings.Average(r => r.Fairness), 2) : 0;
        var averageOverall = ratings.Any() ? Math.Round(ratings.Average(r => r.Overall), 2) : 0;

        var ratingsEntity = new Ratings
        {
            Teaching = averageTeaching,
            Clarity = averageClarity,
            Availability = averageAvailability,
            Fairness = averageFairness,
            Overall = averageOverall,
            ProfessorId = professorId
        };

        return ratingsEntity!;
    }
}
