namespace JN.Search.Application.Features.Search.Models.ResponseModels;

public sealed record SearchServicesResult(int Id, string Name, SearchServicesPosition Position, int Score, string Distance);
