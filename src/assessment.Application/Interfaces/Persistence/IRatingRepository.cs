using assessment.Domain.Entities;

namespace assessment.Application.Interfaces.Persistence;

public interface IRatingRepository : IGenericRepository<Ratings>
{
    public Task<Ratings> GetByProfessorId(int professorId);
}
