using JN.Search.Application.Features.Search.Models.ResponseModels;

namespace JN.Search.Application.Features.Search.Interfaces;

public interface IServiceSearchService
{
    Task<SearchServicesResponse> SearchAsync(string name, double lat, double lng, CancellationToken cancellationToken);
}
