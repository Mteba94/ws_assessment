using assessment.Application.Abstractions.Messaging;

namespace assessment.Application.UseCase.Questions.Command.Create;

public sealed class CreateQuestionCommand : ICommand<bool>
{
    public string Key { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
