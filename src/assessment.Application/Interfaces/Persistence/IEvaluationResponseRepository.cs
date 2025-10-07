using assessment.Domain.Entities;

namespace assessment.Application.Interfaces.Persistence;

public interface IEvaluationResponseRepository : IGenericRepository<EvaluationResponse>
{
    Task<bool> RegistrarEvaluationResponse(IEnumerable<EvaluationResponse> evaluationResponses);
    Task<IEnumerable<EvaluationResponse>> GetByProfessorIdAsync(int professorId);
    Task<IEnumerable<EvaluationResponse>> GetByProfessorLast6MonthsAsync(int professorId);
}
