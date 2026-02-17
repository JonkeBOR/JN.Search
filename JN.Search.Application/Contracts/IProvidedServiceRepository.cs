using JN.Search.Domain.Entities;

namespace JN.Search.Application.Contracts;

public interface IProvidedServiceRepository
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ProvidedService>> SearchByNameAsync(string normalizedName, CancellationToken cancellationToken);
}
