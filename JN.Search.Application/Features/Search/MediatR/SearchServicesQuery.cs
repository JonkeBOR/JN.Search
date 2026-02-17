using JN.Search.Application.Features.Search.Models.ResponseModels;
using MediatR;

namespace JN.Search.Application.Features.Search.MediatR;

public sealed record SearchServicesQuery : IRequest<SearchServicesResponse>
{
    public SearchServicesQuery(string name, double lat, double lng)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }

        if (lat is < -90.0 or > 90.0)
        {
            throw new ArgumentOutOfRangeException(nameof(lat));
        }

        if (lng is < -180.0 or > 180.0)
        {
            throw new ArgumentOutOfRangeException(nameof(lng));
        }

        Name = name;
        Lat = lat;
        Lng = lng;
    }

    public string Name { get; }
    public double Lat { get; }
    public double Lng { get; }
}
