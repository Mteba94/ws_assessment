using assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assessment.Infraestructure.Persistence.Context.Configurations;

internal sealed class RatingsConfiguration : IEntityTypeConfiguration<Ratings>
{
    public void Configure(EntityTypeBuilder<Ratings> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("RatingsId");

        builder.Property(r => r.Teaching)
                .HasColumnType("decimal(3,2)")
                .IsRequired();

        builder.Property(r => r.Clarity)
            .HasColumnType("decimal(3,2)")
            .IsRequired();

        builder.Property(r => r.Availability)
            .HasColumnType("decimal(3,2)")
            .IsRequired();

        builder.Property(r => r.Fairness)
            .HasColumnType("decimal(3,2)")
            .IsRequired();

        builder.Property(r => r.Overall)
            .HasColumnType("decimal(3,2)")
            .IsRequired();
    }
}
