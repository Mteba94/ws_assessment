using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Dtos.Professor;
using assessment.Application.Interfaces.Services;
using assessment.Utilities.Static;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace assessment.Application.UseCase.Professors.Queries.GetAll;

internal sealed class GetAllProfessorHandler(IUnitOfWork unitOfWork, IOrderingQuery orderingQuery) : IQueryHandler<GetAllProfessorQuery, IEnumerable<ProfessorResponseDTO>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderingQuery _orderingQuery = orderingQuery;

    public async Task<BaseResponse<IEnumerable<ProfessorResponseDTO>>> Handle(GetAllProfessorQuery query, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProfessorResponseDTO>>();

        try
        {

            var professors = _unitOfWork.Professor
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
                //users = users.Where(u => stateFilter.Contains(u.Estado.ToString()));
            }

            query.Sort ??= "Id";

            var items = await _orderingQuery.Ordering(query, professors)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await professors.CountAsync(cancellationToken);
            response.Data = items.Adapt<IEnumerable<ProfessorResponseDTO>>();
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
