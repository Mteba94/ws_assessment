using assessment.Domain.Entities;

namespace assessment.Application.Interfaces.Persistence;

public interface IEvaluationsRepository : IGenericRepository<Evaluation>
{
    public Task<IEnumerable<Evaluation>> GetByProfessorId(int professorId);
    public Task<IEnumerable<Evaluation>> GetRecentComments(int professorId);
}
