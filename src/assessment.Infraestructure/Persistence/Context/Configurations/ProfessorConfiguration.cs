using assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assessment.Infraestructure.Persistence.Context.Configurations;

internal sealed class ProfessorConfiguration : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("ProfessorId");

        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

        builder.Property(p => p.Course)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Image)
            .HasMaxLength(255);

    }
}
