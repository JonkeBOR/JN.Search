using JN.Search.Application.Features.Search.Helpers;
using JN.Search.Application.Features.Search.Models.ResponseModels;
using JN.Search.Domain.Entities;

namespace JN.Search.Application.Features.Search.Mappers;

public static class SearchServicesResultMapper
{
    public static SearchServicesResult Map(ProvidedService service, double originLat, double originLng, int score)
    {
        var km = HaversineDistanceHelper.Km(originLat, originLng, service.Latitude, service.Longitude);

        return new SearchServicesResult(
            Id: service.Id,
            Name: service.Name,
            Position: new SearchServicesPosition(service.Latitude, service.Longitude),
            Score: score,
            Distance: DistanceStringHelper.Format(km));
    }
}
