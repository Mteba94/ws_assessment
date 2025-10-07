using assessment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment.Application.Interfaces.Persistence;

public interface IEvaluationsRepository : IGenericRepository<Evaluation>
{
    public Task<Evaluation> GetByProfessorId(int professorId);
}
