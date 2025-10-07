using assessment.Application.Abstractions.Messaging;

namespace assessment.Application.UseCase.Professors.Command.Create;

public sealed class CreateProfessorCommand : ICommand<bool>
{
    public string Name { get; set; } = null!;
    public string Course { get; set; } = null!;
    public string? Image { get; set; }
}
