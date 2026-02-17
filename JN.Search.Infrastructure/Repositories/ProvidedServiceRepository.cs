using JN.Search.Application.Interfaces;
using JN.Search.Domain.Entities;
using JN.Search.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JN.Search.Infrastructure.Repositories;

public sealed class ProvidedServiceRepository : IProvidedServiceRepository
{
    private readonly AppDbContext _db;

    public ProvidedServiceRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return _db.ProvidedServices.AsNoTracking().CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProvidedService>> SearchByNameAsync(string normalizedName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            return [];
        }

        var candidates = await _db.ProvidedServices
            .AsNoTracking()
            .Where(x => EF.Functions.Like(x.Name, $"%{normalizedName}%"))
            .ToListAsync(cancellationToken);

        return candidates;
    }
}
