﻿using assessment.Application.Commons.Bases;

namespace assessment.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<BaseResponse<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
