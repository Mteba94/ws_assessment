using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Interfaces.Services;
using assessment.Domain.Entities;
using assessment.Utilities.Static;
using Mapster;

namespace assessment.Application.UseCase.Professors.Command.Create;

internal sealed class CreateProfessorHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateProfessorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;

    public async Task<BaseResponse<bool>> Handle(CreateProfessorCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateProfessorAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateProfessorAsync(CreateProfessorCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var professor = command.Adapt<Professor>();

            await _unitOfWork.Professor.CreateAsync(professor);
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
