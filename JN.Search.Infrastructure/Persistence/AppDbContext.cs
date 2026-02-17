
using JN.Search.Domain.Entities;
using JN.Search.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace JN.Search.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProvidedService> ProvidedServices => Set<ProvidedService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProvidedServiceConfiguration());
    }
}
