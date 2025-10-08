using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Dtos.Comment;
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
            var ratings = await _unitOfWork.Ratings.GetByProfessorId(query.ProfessorId);
            var comments = await _unitOfWork.Evaluation.GetRecentComments(query.ProfessorId);
            var totalComments = await _unitOfWork.Evaluation.GetByProfessorId(query.ProfessorId);

            if (professor == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                return response;
            }

            var commentsCount = totalComments.Count();

            if(commentsCount == 0)
            {
                var sentimentStats = new CommentStatsDTO
                {
                    TotalComments = 0,
                    PositiveCount = 0,
                    NegativeCount = 0,
                    PositivePercentage = 0,
                    NegativePercentage = 0
                };
            }

            var positive = totalComments.Count(c => c.Sentiment!.Equals("positive", StringComparison.OrdinalIgnoreCase));
            var negative = totalComments.Count(c => c.Sentiment!.Equals("negative", StringComparison.OrdinalIgnoreCase));

            double positivePercentage = 0;
            double negativePercentage = 0;

            if (commentsCount > 0)
            {
                positivePercentage = Math.Round((double)positive / commentsCount * 100, 2);
                negativePercentage = Math.Round((double)negative / commentsCount * 100, 2);
            }

            var commentStats = new CommentStatsDTO
            {
                TotalComments = commentsCount,
                PositiveCount = positive,
                NegativeCount = negative,
                PositivePercentage = positivePercentage,
                NegativePercentage = negativePercentage
            };

            var professorDashboard = professor.Adapt<ProfessorDashboardDTO>();

            professorDashboard.Stats = stats.Adapt<StatsDto>();
            professorDashboard.Ratings = ratings.Adapt<RatingsDto>();
            professorDashboard.RecentComments = comments.Select(c => c.CommentText).ToList()!;
            professorDashboard.CommentsStats = commentStats.Adapt<CommentsStatsDTO>();

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
