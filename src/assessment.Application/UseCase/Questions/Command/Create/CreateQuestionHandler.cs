using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Interfaces.Services;
using assessment.Domain.Entities;
using assessment.Utilities.Static;
using Mapster;

namespace assessment.Application.UseCase.Questions.Command.Create;

internal sealed class CreateQuestionHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateQuestionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateQuestionAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateQuestionAsync(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var question = command.Adapt<Question>();

            await _unitOfWork.Question.CreateAsync(question);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
