namespace assessment.Application.Dtos.Comment;

public class CommentStatsDTO
{
    public int TotalComments { get; set; }
    public int PositiveCount { get; set; }
    public int NegativeCount { get; set; }
    public double PositivePercentage { get; set; }
    public double NegativePercentage { get; set; }
}
