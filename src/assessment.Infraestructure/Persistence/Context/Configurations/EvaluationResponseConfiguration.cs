using assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assessment.Infraestructure.Persistence.Context.Configurations;

internal sealed class EvaluationResponseConfiguration : IEntityTypeConfiguration<EvaluationResponse>
{
    public void Configure(EntityTypeBuilder<EvaluationResponse> builder)
    {
        builder.HasKey(er => er.Id);

        builder.Property(er => er.Id)
            .HasColumnName("EvaluationResponseId");
    }
}
