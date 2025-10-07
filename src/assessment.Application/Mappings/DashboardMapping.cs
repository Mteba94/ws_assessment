using assessment.Application.Dtos.Dashboard;
using assessment.Domain.Entities;
using Mapster;

namespace assessment.Application.Mappings;

public class DashboardMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Professor, ProfessorDashboardDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Course, src => src.Course)
            .Map(dest => dest.Avatar, src => src.Image ?? "https://images.unsplash.com/photo-1494790108755-2616b612b786?w=150&h=150&fit=crop&crop=face");

        // 🔹 Mapear ProfessorStats -> StatsDto
        config.NewConfig<ProfessorStats, StatsDto>()
            .Map(dest => dest.TotalResponses, src => src.TotalResponses)
            .Map(dest => dest.AverageRating, src => Math.Round(src.AverageRating, 2))
            .Map(dest => dest.CompletionRate, src => Math.Round(src.CompletionRate, 2))
            .Map(dest => dest.SemesterAverage, src => Math.Round(src.SemesterAverage, 2));

        // 🔹 Mapear Ratings -> RatingsDto
        config.NewConfig<Ratings, RatingsDto>()
            .Map(dest => dest.Teaching, src => src.Teaching)
            .Map(dest => dest.Clarity, src => src.Clarity)
            .Map(dest => dest.Availability, src => src.Availability)
            .Map(dest => dest.Fairness, src => src.Fairness)
            .Map(dest => dest.Overall, src => src.Overall);
    }
}
