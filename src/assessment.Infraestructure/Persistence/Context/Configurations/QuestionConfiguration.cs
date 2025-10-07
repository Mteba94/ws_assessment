using assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assessment.Infraestructure.Persistence.Context.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
            .HasColumnName("QuestionId");

        builder.Property(q => q.Key)
                .IsRequired()
                .HasMaxLength(50);

        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(q => q.Description)
            .HasMaxLength(255);
    }
}
