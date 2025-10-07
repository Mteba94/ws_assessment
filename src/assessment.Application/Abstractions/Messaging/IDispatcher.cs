using assessment.Application.Commons.Bases;

namespace assessment.Application.Abstractions.Messaging;

public interface IDispatcher
{
    Task<BaseResponse<TResponse>> Dispatch<TRequest, TResponse>(
        TRequest request, CancellationToken cancellationToken) where TRequest : IRequest<TResponse>;
}
