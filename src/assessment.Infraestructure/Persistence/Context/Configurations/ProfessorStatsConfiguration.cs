using assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assessment.Infraestructure.Persistence.Context.Configurations;

internal sealed class ProfessorStatsConfiguration : IEntityTypeConfiguration<ProfessorStats>
{
    public void Configure(EntityTypeBuilder<ProfessorStats> builder)
    {
        builder.HasKey(ps => ps.Id);

        builder.Property(ps => ps.Id)
            .HasColumnName("ProfessorStatsId");

        builder.Property(ps => ps.TotalResponses)
                .IsRequired();

        builder.Property(ps => ps.AverageRating)
            .HasColumnType("decimal(4,2)");

        builder.Property(ps => ps.CompletionRate)
            .HasColumnType("decimal(5,2)");

        builder.Property(ps => ps.SemesterAverage)
            .HasColumnType("decimal(4,2)");

    }
}
