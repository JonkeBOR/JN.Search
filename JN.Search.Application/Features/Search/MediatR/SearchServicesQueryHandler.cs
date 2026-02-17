using JN.Search.Application.Features.Search.Helpers;
using JN.Search.Application.Features.Search.Mappers;
using JN.Search.Application.Features.Search.Models.ResponseModels;
using JN.Search.Application.Interfaces;
using MediatR;

namespace JN.Search.Application.Features.Search.MediatR;

public sealed class SearchServicesQueryHandler : IRequestHandler<SearchServicesQuery, SearchServicesResponse>
{
    private const int MinimumScore = 20;

    private readonly IProvidedServiceRepository _repository;

    public SearchServicesQueryHandler(IProvidedServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<SearchServicesResponse> Handle(SearchServicesQuery request, CancellationToken cancellationToken)
    {
        var normalizedQuery = StringNormalizer.Normalize(request.Name);

        var totalDocumentsTask = _repository.CountAsync(cancellationToken);
        var candidatesTask = _repository.SearchByNameAsync(normalizedQuery, cancellationToken);

        await Task.WhenAll(totalDocumentsTask, candidatesTask);

        var candidates = candidatesTask.Result;

        var scored = candidates
            .Select(s =>
            {
                var normalizedCandidate = StringNormalizer.Normalize(s.Name);
                var score = ServiceNameScoringHelper.Score(normalizedQuery, normalizedCandidate);
                return (service: s, score);
            })
            .Where(x => x.score >= MinimumScore)
            .Select(x =>
            {
                var result = x.service.Map(request.Lat, request.Lng, x.score);
                var distanceKm = HaversineDistanceHelper.Km(
                    request.Lat,
                    request.Lng,
                    x.service.Latitude,
                    x.service.Longitude);

                return (result, x.score, distanceKm);
            })
            .OrderByDescending(x => x.score)
            .ThenBy(x => x.distanceKm)
            .ThenBy(x => x.result.Name, StringComparer.Ordinal)
            .ToArray();

        var totalHits = scored.Length;

        var results = scored
            .Take(request.Limit)
            .Select(x => x.result)
            .ToArray();

        return new SearchServicesResponse(
            TotalHits: totalHits,
            TotalDocuments: totalDocumentsTask.Result,
            Results: results);
    }
}
