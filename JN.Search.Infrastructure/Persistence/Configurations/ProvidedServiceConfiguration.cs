using JN.Search.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JN.Search.Infrastructure.Persistence.Configurations;

public sealed class ProvidedServiceConfiguration : IEntityTypeConfiguration<ProvidedService>
{
    public void Configure(EntityTypeBuilder<ProvidedService> builder)
    {
        builder.ToTable("ProvidedServices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Latitude).IsRequired();
        builder.Property(x => x.Longitude).IsRequired();
    }
}
