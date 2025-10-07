using assessment.Application.Abstractions.Messaging;
using assessment.Application.Commons.Bases;
using assessment.Application.Interfaces.Services;
using assessment.Domain.Entities;
using assessment.Utilities.Static;
using Mapster;

namespace assessment.Application.UseCase.Evaluations.Command.Create;

internal sealed class CreateEvaluationHandler(IUnitOfWork unitOfWork, HandlerExecutor executor) : ICommandHandler<CreateEvaluationCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly HandlerExecutor _executor = executor;
    public async Task<BaseResponse<bool>> Handle(CreateEvaluationCommand command, CancellationToken cancellationToken)
    {
        return await _executor.ExecuteAsync(command, () => CreateEvaluationAsync(command, cancellationToken), cancellationToken);
    }

    private async Task<BaseResponse<bool>> CreateEvaluationAsync(CreateEvaluationCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();
        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var evaluacion = command.Adapt<Evaluation>();

            await _unitOfWork.Evaluation.CreateAsync(evaluacion);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var evaluationResponse = command.EvaluationResponseRequests
                .Select(er => new EvaluationResponse
                {
                    EvaluationId = evaluacion.Id,
                    QuestionId = er.QuestionId,
                    Score = er.Score
                }).ToList();

            var ratings = new Ratings
            {
                Teaching = command.RatingsRequest.Teaching,
                Clarity = command.RatingsRequest.Clarity,
                Availability = command.RatingsRequest.Availability,
                Fairness = command.RatingsRequest.Fairness,
                Overall = command.RatingsRequest.Overall,
                ProfessorId = command.ProfessorId,
            };

            await _unitOfWork.EvaluationResponse.RegistrarEvaluationResponse(evaluationResponse);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _unitOfWork.Ratings.CreateAsync(ratings);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var existingStats = await _unitOfWork.ProfessorStats.GetByIdAsync(command.ProfessorId);

            var ratingsList = new List<double>
            {
                command.RatingsRequest.Teaching,
                command.RatingsRequest.Clarity,
                command.RatingsRequest.Availability,
                command.RatingsRequest.Fairness,
                command.RatingsRequest.Overall
            };

            //var averageRating = ratingsList.Average();
            var totalExpectedStudents = 30;

            var evaluationsCount = await _unitOfWork.EvaluationResponse.GetByProfessorIdAsync(command.ProfessorId);

            var totalResponses = evaluationsCount.Count();

            var averageRating = evaluationsCount.Any()
                ? evaluationsCount.Average(er => er.Score)
                : 0;

            var semesterEvaluations = await _unitOfWork.EvaluationResponse.GetByProfessorLast6MonthsAsync(command.ProfessorId);

            var semesterAverage = semesterEvaluations.Any()
                ? semesterEvaluations.Average(er => er.Score)
                : averageRating;

            var completionRate = totalExpectedStudents == 0
                ? 0
                : (totalResponses / (double)totalExpectedStudents) * 100;

            if (existingStats == null)
            {
                var newStats = new ProfessorStats
                {
                    ProfessorId = command.ProfessorId,
                    TotalResponses = 1,
                    AverageRating = averageRating,
                    CompletionRate = (1 / totalExpectedStudents) * 100,
                    SemesterAverage = averageRating,

                };

                await _unitOfWork.ProfessorStats.CreateAsync(newStats);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                existingStats.TotalResponses += 1;
                existingStats.AverageRating = ((existingStats.AverageRating * (existingStats.TotalResponses - 1)) + averageRating) / existingStats.TotalResponses;
                existingStats.SemesterAverage = existingStats.AverageRating;
                existingStats.CompletionRate = ((double)existingStats.TotalResponses / totalExpectedStudents) * 100;

                _unitOfWork.ProfessorStats.Update(existingStats);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
