using assessment.Domain.Entities;

namespace assessment.Application.Interfaces.Persistence;

public interface IProfessorStatsRepository : IGenericRepository<ProfessorStats>
{
    public Task<ProfessorStats> GetByProfessorId(int professorId);
}
