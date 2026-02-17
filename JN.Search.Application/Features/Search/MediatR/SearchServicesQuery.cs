using JN.Search.Application.Features.Search.Models.ResponseModels;
using MediatR;

namespace JN.Search.Application.Features.Search.MediatR;

public sealed record SearchServicesQuery(string Name, double Lat, double Lng) : IRequest<SearchServicesResponse>;
