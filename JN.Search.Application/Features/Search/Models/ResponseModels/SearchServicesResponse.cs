namespace JN.Search.Application.Features.Search.Models.ResponseModels;

public sealed record SearchServicesResponse(int TotalHits, int TotalDocuments, IReadOnlyList<SearchServicesResult> Results);
