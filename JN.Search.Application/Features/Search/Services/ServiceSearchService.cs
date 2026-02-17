using JN.Search.Application.Contracts;
using JN.Search.Application.Features.Search.Helpers;
using JN.Search.Application.Features.Search.Interfaces;
using JN.Search.Application.Features.Search.Mappers;
using JN.Search.Application.Features.Search.Models.ResponseModels;

namespace JN.Search.Application.Features.Search.Services;

public sealed class ServiceSearchService : IServiceSearchService
{
    private const int MinimumScore = 20;

    private readonly IProvidedServiceRepository _repository;

    public ServiceSearchService(IProvidedServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<SearchServicesResponse> SearchAsync(string name, double lat, double lng, CancellationToken cancellationToken)
    {
        var normalizedQuery = StringNormalizer.Normalize(name);

        var totalDocumentsTask = _repository.CountAsync(cancellationToken);
        var candidatesTask = _repository.SearchByNameAsync(normalizedQuery, cancellationToken);

        await Task.WhenAll(totalDocumentsTask, candidatesTask);

        var scored = candidatesTask.Result
            .Select(s => (service: s, score: ScoreCandidate(normalizedQuery, s.Name)))
            .Where(x => x.score >= MinimumScore)
            .Select(x => Project(lat, lng, x.service, x.score))
            .OrderByDescending(x => x.score)
            .ThenBy(x => x.distanceKm)
            .ThenBy(x => x.result.Name, StringComparer.Ordinal)
            .ToArray();

        var totalHits = scored.Length;

        var results = scored
            .Select(x => x.result)
            .ToArray();

        return new SearchServicesResponse(
            TotalHits: totalHits,
            TotalDocuments: totalDocumentsTask.Result,
            Results: results);
    }

    private static int ScoreCandidate(string normalizedQuery, string candidateName)
    {
        var normalizedCandidate = StringNormalizer.Normalize(candidateName);
        return ServiceNameScoringHelper.Score(normalizedQuery, normalizedCandidate);
    }

    private static (SearchServicesResult result, int score, double distanceKm) Project(double originLat, double originLng, Domain.Entities.ProvidedService service, int score)
    {
        var distanceKm = HaversineDistanceHelper.Km(originLat, originLng, service.Latitude, service.Longitude);
        return (SearchServicesResultMapper.Map(service, originLat, originLng, score), score, distanceKm);
    }
}
