namespace assessment.Application.Dtos.Dashboard;

public class ProfessorDashboardDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Course { get; set; } = default!;
    public string Avatar { get; set; } = default!;

    public StatsDto Stats { get; set; } = default!;
    public RatingsDto Ratings { get; set; } = default!;
    public List<string> RecentComments { get; set; } = new();
    public CommentsStatsDTO? CommentsStats { get; set; }
}

public class StatsDto
{
    public int TotalResponses { get; set; }
    public double AverageRating { get; set; }
    public double CompletionRate { get; set; }
    public double SemesterAverage { get; set; }
}

public class RatingsDto
{
    public double Teaching { get; set; }
    public double Clarity { get; set; }
    public double Availability { get; set; }
    public double Fairness { get; set; }
    public double Overall { get; set; }
}

public class CommentsStatsDTO
{
    public int TotalComments { get; set; }
    public int PositiveCount { get; set; }
    public int NegativeCount { get; set; }
    public double PositivePercentage { get; set; }
    public double NegativePercentage { get; set; }
}
