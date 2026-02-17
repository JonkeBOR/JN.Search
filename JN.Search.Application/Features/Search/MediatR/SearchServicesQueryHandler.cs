using JN.Search.Application.Features.Search.Interfaces;
using JN.Search.Application.Features.Search.Models.ResponseModels;
using MediatR;

namespace JN.Search.Application.Features.Search.MediatR;

public sealed class SearchServicesQueryHandler : IRequestHandler<SearchServicesQuery, SearchServicesResponse>
{
    private readonly IServiceSearchService _service;

    public SearchServicesQueryHandler(IServiceSearchService service)
    {
        _service = service;
    }

    public async Task<SearchServicesResponse> Handle(SearchServicesQuery request, CancellationToken cancellationToken)
    {
        return await _service.SearchAsync(request.Name, request.Lat, request.Lng, cancellationToken);
    }
}
