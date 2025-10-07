namespace assessment.Application.Dtos.Professor;

public class ProfessorResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Course { get; set; } = null!;
    public string? Image { get; set; }
    public int State { get; set; }
    public string? StateDescription { get; set; }
}
