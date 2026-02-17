using System.Text.Json.Serialization;

namespace JN.Search.Application.Features.Search.Models.ResponseModels;

public sealed record SearchServicesResponse(
    [property: JsonPropertyName("TotalHits")] int TotalHits,
    [property: JsonPropertyName("TotalDocuments")] int TotalDocuments,
    [property: JsonPropertyName("Results")] IReadOnlyList<SearchServicesResult> Results);

public sealed record SearchServicesResult(
    [property: JsonPropertyName("Id")] int Id,
    [property: JsonPropertyName("Name")] string Name,
    [property: JsonPropertyName("Position")] SearchServicesPosition Position,
    [property: JsonPropertyName("Score")] int Score,
    [property: JsonPropertyName("Distance")] string Distance);

public sealed record SearchServicesPosition(
    [property: JsonPropertyName("Lat")] double Lat,
    [property: JsonPropertyName("Lng")] double Lng);
