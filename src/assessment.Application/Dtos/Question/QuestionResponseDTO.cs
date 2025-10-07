namespace assessment.Application.Dtos.Question;

public class QuestionResponseDTO
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
    public string? StateDescription { get; set; }
}
