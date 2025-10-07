using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Dtos.Dashboard;
using assessment.Application.Interfaces.Services;
using assessment.Utilities.Static;
using Mapster;

namespace assessment.Application.UseCase.Professors.Queries.Dashboard;

internal sealed class DashboardProfessorHandler(IUnitOfWork unitOfWork) : IQueryHandler<DashboardProfessorQuery, ProfessorDashboardDTO>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<BaseResponse<ProfessorDashboardDTO>> Handle(DashboardProfessorQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ProfessorDashboardDTO>();

		try
		{
			var professor = await _unitOfWork.Professor.GetByIdAsync(query.ProfessorId);
			var stats = await _unitOfWork.ProfessorStats.GetByProfessorId(query.ProfessorId);

            if(professor == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            var professorDashboard =  professor.Adapt<ProfessorDashboardDTO>();

            professorDashboard.Stats = stats.Adapt<StatsDto>();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_QUERY;
            response.Data = professorDashboard;
        }
		catch (Exception)
		{
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
		}

		return response;
    }
}
