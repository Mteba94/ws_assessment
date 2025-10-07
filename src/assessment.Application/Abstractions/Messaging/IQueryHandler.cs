using assessment.Application.Commons.Bases;

namespace assessment.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<BaseResponse<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
