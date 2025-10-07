using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Dtos.Question;
using assessment.Application.Interfaces.Services;
using assessment.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace assessment.Application.UseCase.Questions.Queries.GetAll;

internal sealed class GetAllQuestionHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllQuestionQuery, IEnumerable<QuestionResponseDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;
    public async Task<BaseResponse<IEnumerable<QuestionResponseDTO>>> Handle(GetAllQuestionQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<QuestionResponseDTO>>();

        try
        {
            var questions = _unitOfWork.Question
                .GetAllQueryable();

            if (query.NumFilter is not null && !string.IsNullOrEmpty(query.TextFilter))
            {
                switch (query.NumFilter)
                {
                    case 1:
                        //users = users.Where(u => u.Pnombre.Contains(query.TextFilter));
                        break;
                }
            }

            if (query.StateFilter is not null)
            {
                //var stateFilter = Helper.SplitStateFilter(query.StateFilter);
                //tags = tags.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, questions)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await questions.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<QuestionResponseDTO>>();
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
